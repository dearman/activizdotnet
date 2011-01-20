# This is the translator that converts Tcl test to python.
# Not all Tcl test are translatable. 
# To ensure that a test can be translated :
# 1) do not use Tcl arrays
# 2) do not use string substitution except in variable names
#    eg. obj${i} GetOutput is okay
#        obj12 GetOutputAs${i} is not okay.
# 3) do not use expr within expr. As such it is typically superflous.
# 4) event handler procedures in Python take 2 arguments, hence,
#    define the Tcl event handlers with 2 default arguments.
# 5) define procedures before using them or setting them on VTK objects
#    as callbacks.
# 6) do not use puts etc.
# 7) always quote strings such as filenames, arguments etc.


import sys
import re
import string
import vtkColors
import TclToCsHashMap
import XML_Parser
import vtkCommandList

typeValues = TclToCsHashMap.TclToCsHashMap()
typeValues.AddItem("Object",3)
typeValues.AddItem("object",3)
typeValues.AddItem("String",2)
typeValues.AddItem("string",2)
typeValues.AddItem("IntPtr",2)
typeValues.AddItem("double",1)
typeValues.AddItem("int",0)
typeValues.AddItem("long",0)
typeValues.AddItem("uint",0)

for i in range(0, len(sys.argv)):
  if sys.argv[i] == '-A' and i < len(sys.argv)-1:
    sys.path = [sys.argv[i+1]] + sys.path
    pass
  pass

import vtkTclParser


reVariable = re.compile("^([+\-])?\$([^\$\{\}]+)$")
reCompoundVariable = re.compile("\$(?:([^\$\}\{]+)|\{([^\$\}]+)\})")
vtk_colors = vtkColors.vtkColors()

class vtkTclToCsConvertor(vtkTclParser.vtkTclParser):

    def __init__(self):
      vtkTclParser.vtkTclParser.__init__(self)
      self.parent = "SELF"
      self.methods_untranslated = [] #stores raw method data 
      self.nontranslated_methods = [] #stores method declarations of things like lindex and anything that does not come from the tcl script
      self.output = ""
      self.variable_values = TclToCsHashMap.TclToCsHashMap()
      #self.variable_values.AddItem("System.Environment.GetEnvironmentVariable(\"VTK_DATA_ROOT\")","(string)")
      self.indent = ""
      self.parent_command = ""
      self._procedure_list = []
      self.class_list = []
      self.name_space = "vtk"
      hints = file(sys.argv[0].replace("vtkTclToCsConvertor.py","")+"refdouble.hints","r")
      self.refdouble_hints = hints.read().replace(" ","").replace("\n","").split("-")
      hints.close()
      self.global_variables = []
      self.global_variables.append("static string VTK_DATA_ROOT;\n")
      self.global_variables.append("static int threshold;\n");
      self.methods = []#stores translated method data
      self.addobserver_methods = []
      index = [output_file.rfind('\\'),output_file.rfind('/')][output_file.rfind('\\')<output_file.rfind('/')]#choses the bigest index
      self.file_name = (output_file[(index+1):(output_file.rfind("."))])
      self.array_index_max = TclToCsHashMap.TclToCsHashMap()
      self.using_statements = ["using System;\n","using Kitware.VTK;\n"]
      self.abstract_classes = ["vtkPolyDataMapper","vtkRenderer","vtkRenderWindow", "vtkImageMapper"]
      pass

    def post_process(self):
      invalid_words = ["base","bool", "operator","params"]#TODO'is' is also an invalid word but there needs to be a better way to find it
      output = self.output
      for word in invalid_words:
        output = output.replace(word,word+"1");
      output = output.replace(";;",";")#gets rid of any double ';'
      output = output.replace("rtTester","TclToCsScriptTestDriver") #rtTester is tcl TclToCSScriptTestDriver is CS
      string = output.replace(" ","")
      self.output = output
      pass

    #prints the class name and main method
    def print_header(self, prefix_content=""):
      self.handle_command("""// input file is """+input_file+"""
// output file is """+output_file+"""
/// <summary>
/// The testing class derived from %s
/// </summary>
public class %sClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void %s(String [] argv)
  {
  //Prefix Content is: "%s"
  """ % (self.file_name,self.file_name,self.file_name, prefix_content))
      self.indent = "  "
      pass
    #use this function whenever a new instance of Tcl2CsConveter is created on 
    #the second instance. This will take any global variables declared there and
    #put them in the bigger global list
    def update_globals(self, p):
      self.global_variables.extend(p.global_variables)
      self.nontranslated_methods.extend(p.nontranslated_methods)
      self.using_statements.extend(p.using_statements)
      self.methods.extend(p.methods)
      self.addobserver_methods.extend(p.addobserver_methods)
      self.variable_values.CombineWith(p.variable_values)
      self.methods = self.remove_duplicates(self.methods)
      self.addobserver_methods = self.remove_duplicates(self.addobserver_methods)
      self.nontranslated_methods = self.remove_duplicates(self.nontranslated_methods)
      self.global_variables = self.remove_duplicates(self.global_variables)
      self.using_statements = self.remove_duplicates(self.using_statements)
      global_names = []
      for g in self.global_variables:
        if len(g.split())> 2:
          global_names.append(g.split()[2])
          pass
        pass
      global_names = self.remove_duplicates(global_names)
      temp_global_variables = []
      #removes duplicates of global variables with different object types
      if len(global_names) != len(self.global_variables):
        #In this instance we want double to be more specific than an int
        typeValues.values[typeValues.GetItemIndex("double")]=0
        typeValues.values[typeValues.GetItemIndex("int")]=1
        for gn in global_names:
          temp_global_type = ""
          index_list = []
          for i in range(len(self.global_variables)):
            if len(self.global_variables[i].split())>2 and gn == self.global_variables[i].split()[2]:
              if temp_global_type == "":
                temp_global_type = self.global_variables[i].split()[1]
                index_list.append(i)
                pass
              else:
                index_list.append(i)
                for j in index_list:
                  self.global_variables[j] = self.global_variables[j].replace(self.global_variables[j].split()[1],
                  [temp_global_type,self.global_variables[i].split()[1]]
                  [typeValues.GetItem(temp_global_type) > typeValues.GetItem(self.global_variables[i].split()[1])])
                  pass
                pass
              pass
            pass
          pass
        #Put the values back after we are done
        typeValues.values[typeValues.GetItemIndex("double")]=1
        typeValues.values[typeValues.GetItemIndex("int")]=0
        pass
      self.global_variables = self.remove_duplicates(self.global_variables)
      self.methods = self.remove_duplicates(self.methods)
      pass

    def remove_duplicates(self, seq): 
      seen = {}
      result = []
      for item in seq:
        marker = item
        if marker in seen: 
          continue
        seen[marker] = 1
        result.append(item)
        pass
      return result

    def look_up_variable_type(self, var):
      if var in self.class_list:
        return var
      for i in range(len(self.global_variables)):
        if len(self.global_variables[i].split())>2 and var == self.global_variables[i].split()[2].replace(";",""):
          toReturn = self.global_variables[i].split()[1]
          if toReturn.count("[")>0:
            toReturn = toReturn[:toReturn.find("[")]
            pass
          return toReturn
        pass
      return None

    #prints all global variables and methods
    def print_footer(self):
      #clean up all rubish data
      self.global_variables = self.remove_duplicates(self.global_variables)
      
      ##only generate set and get methods for these varibales
      #generate_set_and_get_list = ["iren","renWin","ren","viewer","VTK_DATA_ROOT","threshold"]
      for g in self.global_variables:
        if len(g.split()) >=3 :
          temp_type = g.split()[1]
          temp_name = g.split()[2].replace(";","")
          #if temp_name in generate_set_and_get_list:
          self.nontranslated_methods.append("""
        ///<summary> A Get Method for Static Variables </summary>
        public static """+temp_type+""" Get"""+temp_name+"""()
        {
            return """+temp_name+""";
        }
        """)
          self.nontranslated_methods.append("""
        ///<summary> A Set Method for Static Variables </summary>
        public static void Set"""+temp_name+"""("""+temp_type+""" toSet)
        {
            """+temp_name+""" = toSet;
        }
        """)
          pass
        pass
      #retranslated all the methods in case there was a global variable used that was declared later in the script
      self.methods = []
      temp_untranslated = self.methods_untranslated[:]
     
      for methods in temp_untranslated:
        self.translate_command(methods[0],methods[1])
        pass      
      self.methods.extend(self.nontranslated_methods)
      #clean up more rubbish
      self.global_variables = self.remove_duplicates(self.global_variables)
     
      self.methods = self.remove_duplicates(self.methods)
      self.second_pass()

      tempGlobal = ""
      tempMethods = ""
      tempDispose = "\n"+self.indent+"\t//clean up vtk objects\n"
      
      for i in range(len(self.global_variables)):
        tempGlobal += self.global_variables[i]
        if ((self.global_variables[i]).split())[1] in self.class_list:
          temp_var_name = (self.global_variables[i].split())[2].replace(";","")
          tempDispose+= self.indent+"\tif("+temp_var_name+"!= null){"+temp_var_name+".Dispose();}\n"
          pass
        pass
      for i in range(len(self.methods)):
        tempMethods += self.methods[i]
        pass
      
      tempMethods += "\n"+self.indent+"///<summary>Deletes all static objects created</summary>"+"\n"+self.indent+"public static void deleteAllVTKObjects()\n"+self.indent+"{"+tempDispose+self.indent+"}\n"


      self.handle_command("\n//deleteAllVTKObjects();\n"+self.indent+"}\n"+tempGlobal+"\n"+tempMethods+"\n}\n//--- end of script --//\n")
      
      for i in range(len(self.using_statements)):
        self.output = self.using_statements[i]+self.output
        pass
      pass

    #passes through the code again in case a custom process took unkown type arguments
    def second_pass(self):
      
      for i in range(0,(len(self.methods))):
        if "A procedure translated from the tcl scripts" not in self.methods[i]:
          break
        #compile a regular expression to remove everything but the name
        reg1='.*?'	# Non-greedy match on filler
        reg2='(?:[a-z][a-z]+)'	# Uninteresting: word
        reg3='.*?'	# Non-greedy match on filler
        reg4='(?:[a-z][a-z]+)'	# Uninteresting: word
        reg5='.*?'	# Non-greedy match on filler
        reg6='(?:[a-z][a-z]+)'	# Uninteresting: word
        reg7='.*?'	# Non-greedy match on filler
        reg8='((?:[a-z][a-z]+))'	# Word 1

        rg = re.compile(reg1+reg2+reg3+reg4+reg5+reg6+reg7+reg8,re.IGNORECASE|re.DOTALL)
        regComent0 = re.compile("//.*")
        regComent1 = re.compile("/\*.*\*/",re.DOTALL)
        method = regComent0.sub("",self.methods[i])
        method = regComent1.sub("",method)
        m = rg.search(method.replace("\n",""))
        functionName = ""
        if m:
          word1=m.group(1)
          functionName = word1
          pass
        method_params = (self.methods[i][self.methods[i].find("(")+1:self.methods[i].find(")")]).split(",")
        #no arguments in this method, so ignore it
        if method_params[0].replace(" ","") == "":
              break
        remainingCode = self.output
        #remove comments from the code
        remainingCode = regComent0.sub("",remainingCode)
        remainingCode = regComent1.sub("",remainingCode)
        remainingCode = remainingCode.replace(" ","")
        remainingCode = re.compile(".*argv.Length;.}",re.DOTALL).sub("",remainingCode)
        #parse the generated code
        while functionName+"(" in remainingCode:
          remainingCode = remainingCode[remainingCode.find(functionName):]
          if ";" not in remainingCode:
            break;
          line = remainingCode[:remainingCode.find(";")]
          remainingCode = remainingCode[remainingCode.find(";")+1:]
          arg_str = line[line.find("(")+1:line.rfind(")")]
          arg_arr = arg_str.split(",")
          if len(arg_arr) == 0:
            break
          new_arg_str = ""
          #look at the types of arguments passed into the function
          for j in range(0,len(arg_arr)):
            arg = arg_arr[j]
            type = self.look_up_type(arg)
            #check to make sure that there are argumetns in this function
            if j > 0: 
              new_arg_str+=","
              pass
            if j >= len(method_params):
              break
            sMethods_params = method_params[j].split(" ")
            new_arg_str+= type+" "+sMethods_params[len(sMethods_params)-1]
            pass
          self.methods[i] = self.methods[i][:self.methods[i].find("(")+1]+new_arg_str+self.methods[i][self.methods[i].find(")"):]
          pass
        pass
      pass
        
    def reset(self):
      self.output = ""
      self.indent = ""
      vtkTclParser.vtkTclParser.reset(self)
      self._procedure_list = []
      pass

    def _get_block_parser(self):
      p = vtkTclToCsConvertor()
      p.class_list = self.class_list
      p.name_space = self.name_space
      p.indent = self.indent + "    "
      p._procedure_list = self._procedure_list[:]
      return p

    def translate_block(self, block):
      p = self._get_block_parser()
      p.update_globals(self)
      p.indent += "    "
      block = block.strip()
      if block[0] == "{":
        block = block[:]
        pass
      if block[-1] == "}":
        block = block[:]
        pass
      p.feed(block)
      self.update_globals(p)
      return p.output
    
    def translate_operator(self, op):
      #TCL and C# have the same operators
      if op == "&&":
        return "&&"
      if op == "||":
        return "||"
      return op

    def translate_token(self, token):
      """called to transate every token."""
      if token.find("$") == -1:
        return token
      match =    reVariable.match(token)
      if match:
        result = ""
        if match.group(1) != None:
          result += match.group(1)
          pass
        result += match.group(2)
        return result
      result = "locals()[get_variable_name(\""
      match =    reCompoundVariable.search(token)
      while match:
        result += token[:match.start(0)] + "\", "
        if match.group(1) != None:
          result += match.group(1)
          pass
        else:
          result += match.group(2)
          pass
        result += ", \""
        token = token[match.end(0):]
        match =    reCompoundVariable.search(token)
        pass
      result += token +"\")]"
      tempVar = result[28:]
      tempVar = tempVar.replace(" ","")
      tempVar = tempVar.replace("\",","[",1)
      tempVar = tempVar.replace(",\"_\",",",")
      tempVar = tempVar.replace(",\"\")]","]")
      result = tempVar
      return result

        #attempts to find the type of an expression, return type, or variable
    def look_up_type(self, expr, indent = "----"):
            DEBUGLOOKUPTYPE = False #set to false if you don't want to see the tree

            if expr.count("[")>0 and expr.count("]")>0:
              expr = expr[:expr.find("[")]+expr[expr.rfind("]")+1:]
              pass
            if expr != None and expr.split()!=None and len(expr.split())>1 and expr.split()[0]=="new":
              return expr.split()[1][:expr.split()[1].find("(")]
            if DEBUGLOOKUPTYPE:
              print indent+"{"
              print indent+"expr ="+expr
            
            expr = expr.replace(";","")
            tokens = ["-","+","/","*","%",">>","<<"]
            primitive_return = ""
            if expr.count("(double)")>0 and expr.count("((double)") < expr.count("(double)") and expr.count(",(double)") < expr.count("(double)"):
              primitive_return = "double"
            elif expr.count("(long)")>0 and expr.count("((long)") < expr.count("(long)") and expr.count(",(long)") < expr.count("(long)"):
              primitive_return = "long"
            elif expr.count("(uint)")>0 and expr.count("((uint)") < expr.count("(uint)") and expr.count(",(uint)") < expr.count("(uint)"):
              primitive_return = "uint"
            elif expr.count("(int)")>0 and expr.count("((int)") < expr.count("(int)") and expr.count(",(int)") < expr.count("(int)"):
              primitive_return = "int"
            elif expr.count("(string)")>0 and expr.count("((string)") < expr.count("(string)") and expr.count(",(string)") < expr.count("(string)"):
              primitive_return = "string"
            
            if primitive_return != "":
              if DEBUGLOOKUPTYPE:
                print str(indent)+"Type = "+primitive_return
                print indent+"}"
                pass
              return primitive_return
            if expr == "":
              return ""
            try:
               int((expr.strip()))
               if DEBUGLOOKUPTYPE:
                 print str(indent)+"Type = int"
                 print indent+"}"
               return "int"
            except:
               try:
                 float((expr.strip()))
                 if DEBUGLOOKUPTYPE:
                    print str(indent)+"Type = double"
                    print indent+"}"
                 return "double"
               except:
                 type = "Object"
                 pass
               pass
            
            #---Looks up The File Type in the XML files
           
            classType = self.look_up_variable_type(expr[:expr.find(".")])
            if classType != None and expr.find(".")>-1:
              #gets ride of all arguments within parenthesis
              expr = self.remove_arguments(expr)
              if expr.count("(")>0:
                tempVar = expr[expr.find("(")+1:]
                exprWithoutArgs = expr[:expr.find("(")+1]
                while tempVar[:tempVar.find(")")].count("(")>0:
                  tempVar = tempVar[tempVar.find(")",tempVar.count("("))+1:]
                exprWithoutArgs += tempVar[tempVar.find(")"):]
                tempArg = exprWithoutArgs[exprWithoutArgs.find(".")+1:]
                #---
              else:
                tempArg = expr[expr.find(".")+1:]
              while(tempArg.find(".")>1):
                classType = XML_Parser.look_up_method_return_type(classType.replace("*",""),tempArg[:tempArg.find("(")])
                if classType == None:
                  if DEBUGLOOKUPTYPE:
                    print str(indent)+"Type = None"
                    print indent+"}"
                  return None
                tempArg = tempArg[tempArg.find(".")+1:]
                pass
              classType = XML_Parser.look_up_method_return_type(classType.replace("*",""),tempArg[:tempArg.find("(")])
              if classType == None:
                if DEBUGLOOKUPTYPE:
                    print str(indent)+"Type = None"
                    print indent+"}"
                return None
              #searches for the closing parenthesis to the function in case there are arguments afterwords
              if classType != None:
                expr = tempArg
                if expr.count("(")>0:
                  expr = expr[expr.find("(")+1:]
                  while expr[:expr.find(")")].count("(")>0:
                    expr = expr[expr.find(")",expr.count("("))+1:]
                  expr = expr[expr.find(")")+1:] 
                tempType = self.look_up_type(expr,indent+"----")
                #---
                tempArg = tempArg[tempArg.find(".")+1:]
                if classType.find("vtk")>-1:
                  classType = classType.replace("*","")
                if classType.find("*")>-1:
                 classType = classType.replace("*","[]")
                 # uncomment this and comment the next line when methods return arrays instead of IntPtrs (this has been done)
                 # classType = "IntPtr"
                elif classType == "void":
                  classType = "IntPtr"
                type = [classType,tempType][typeValues.GetItem(classType) < typeValues.GetItem(tempType)]
              else:
                type = "Object"
            if type == "Object" and self.variable_values.GetItem(expr)!=None:
              type = self.look_up_variable_type(expr)
            #---Looks up the File Type after a lindex function
            elif type == "Object" and expr.find("lindex") >- 1:
               type = expr[expr.find("(")+1:expr.find(")(lindex")]
               pass
            #---Checks the type agains anything already in the procedure list
            elif type == "Object" and expr[:expr.find("(")] in self._procedure_list:
              method_name = expr[:expr.find("(")]
              for m in self.methods:
                re1='.*?'	# Non-greedy match on filler
                re2='(?:[a-z][a-z]+)'	# Uninteresting: word
                re3='.*?'	# Non-greedy match on filler
                re4='(?:[a-z][a-z]+)'	# Uninteresting: word
                re5='.*?'	# Non-greedy match on filler
                re6='(?:[a-z][a-z]+)'	# Uninteresting: word
                re7='.*?'	# Non-greedy match on filler
                re8='(?:[a-z][a-z]+)'	# Uninteresting: word
                re9='.*?'	# Non-greedy match on filler
                re10='(?:[a-z][a-z]+)'	# Uninteresting: word
                re11='.*?'	# Non-greedy match on filler
                re12='(?:[a-z][a-z]+)'	# Uninteresting: word
                re13='.*?'	# Non-greedy match on filler
                re14='(?:[a-z][a-z]+)'	# Uninteresting: word
                re15='.*?'	# Non-greedy match on filler
                re16='(?:[a-z][a-z]+)'	# Uninteresting: word
                re17='.*?'	# Non-greedy match on filler
                re18='(?:[a-z][a-z]+)'	# Uninteresting: word
                re19='.*?'	# Non-greedy match on filler
                re20='(?:[a-z][a-z]+)'	# Uninteresting: word
                re21='.*?'	# Non-greedy match on filler
                re22='((?:[a-z][a-z]+))'	# Word 1

                rg = re.compile(re1+re2+re3+re4+re5+re6+re7+re8+re9+re10+re11+re12+re13+re14+re15+re16+re17+re18+re19+re20+re21+re22,re.IGNORECASE|re.DOTALL)

                word = rg.search(m).group(1)
                type = word
            #attempts to find the type by looking at the arguments
            elif type == "Object":
               for i in range(len(tokens)):
                 if expr.find(tokens[i])>-1:
                   #parses parenthesis
                   if expr.find(tokens[i])-expr.find("(")>0 and expr.find("(")>-1:
                     tempType3 = expr[:expr.find("(")]
                     tempType4 = ""
                     tempType5 = expr[expr.find("(")+1:]
                     while tempType5[:expr.find(")")].count("(")>0:
                       tempType4 += tempType5[:tempType5.find(")",tempType5.count("(")+1)]
                       tempType5 = tempType5[tempType5.find(")",tempType5.count("("))+1:]
                     tempType4 = tempType5[:tempType5.find(")")] 
                     tempType5 = tempType5[tempType5.find(")")+1:] 
                     if tempType3[len(tempType3)-1:] in tokens:
                       tempType3 = tempType3[:-1]
                     if tempType3[len(tempType4)-1:] in tokens:
                       tempType3 = tempType3[:-1]
                     if tempType3[len(tempType5)-1:] in tokens:
                       tempType3 = tempType3[:-1]
                     tempType3 = self.look_up_type(tempType3,indent+"----")
                     tempType4 = self.look_up_type(tempType4,indent+"----")
                     tempType5 = self.look_up_type(tempType5,indent+"----")
                     val3 = typeValues.GetItem(tempType3)
                     val4 = typeValues.GetItem(tempType4)
                     val5 = typeValues.GetItem(tempType5)
                     if val3 > val4:
                       if val3> val5:
                         type = tempType3
                     elif val4 > val5:
                       type = tempType4
                     else:
                       type = tempType5
                     
                   #---
                   else:
                     token_length = len(tokens[i])
                     tempType1 = self.look_up_type(expr[expr.find(tokens[i])+token_length:].replace(" ",""),indent+"----")
                     expr = expr[:expr.find(tokens[i])]
                     tempType2 = self.look_up_type(expr.replace(" ",""),indent+"----")
                     type = [tempType1,tempType2][typeValues.GetItem(tempType1) < typeValues.GetItem(tempType2)]
                   pass
                 pass
            if type == "Object":
              if expr[0:1]=="\"" and expr[len(expr)-1:] == "\"":
                type = "string"
            if type == "Object":
                classType = self.look_up_variable_type(expr)
                if classType != None:
                  type = classType
            #special case
            if type == "vtkIdType":
              type = "int"
            if DEBUGLOOKUPTYPE:
              print str(indent)+"Type = "+str(type)
              print indent+"}"
            return type

    def _process_string(self):
        """Internal method: process an string enclosed in quotes"""
        if self._in_process_string:
            return None
        self._in_process_string = True
        result = "\""
        arg = self._get_next_word_in_command(True)
        while arg:
            if (arg[0] == "["):
                arg = arg[1:len(arg)-1]
                result += "\" + (" + arg + ".ToString()) + \""
            else:
                m = reVariable.search(arg)
                while m:
                    result += arg[:m.start(0)] + "\" + "
                    result += arg[m.start(0)+1:m.end(0)] + " + \""
                    arg = arg[m.end():]
                    m = reVariable.search(arg)
                result += arg
            arg = self._get_next_word_in_command(True)
        result += "\""
        self._in_process_string = False
        return result
    
    #instead of looking up a variable with a one in the name we make the 1 a variable in an array
    def process_arrays(self, command):
        if command[0:1] == "#":
           return command
        if "lindex" in command:
          return command
        for i in range(len(self.global_variables)):
          if len(self.global_variables[i].split()) > 2:
            value = self.global_variables[i].split()[2].replace(";","")
            if command.count(value)>0 and self.global_variables[i].split()[1].count("[")>0 and command.count("[")==0:
                if command.count("_")>0:
                  tempCommand = command[:command.find("_")+1]
                  command = "["+command[command.find("_")+1:]
                  command = command.replace("_",",")+"]"
                  command = tempCommand+command

                if command.count("[")==0 and len(value)<len(command):
                  command = value+"["+command[len(value):]+"]"
                pass        
        return command
        
    def translate_command(self, command, arguments):
        command = self.process_arrays(command)
        for i in range(len(arguments)):
            arguments[i] = self.process_arrays(arguments[i])
      #self._error("to translate_command %s %s" % (command, `arguments`))
        #--------------------------------------------------------------
        translated_cmd = ""
      #convert comments
        #--------------------------------------------------------------
        if (len(command) > 0 and command[0] == "#") or command == "tkwait":
          translated_cmd = "//"+command[1:]+str(arguments)
      #convert quotes
        #--------------------------------------------------------------
        elif len(command) > 0 and command[0] == "\"":
            translated_cmd = command
      #convert global variable declarations
        #--------------------------------------------------------------
        elif command == "puts":
            translated_cmd = "//puts skiped"+command+" "+str(arguments)
        #--------------------------------------------------------------
        elif command == "global" and len(arguments) >= 1:
          for i in range(len(arguments)):
            #self.global_variables.append("static Object "+arguments[i]+";\n")
            translated_cmd = "//Global Variable Declaration Skipped"
      #convert evals
        #--------------------------------------------------------------
        elif command == "eval" and len(arguments) > 0:
            translated_cmd = self.translate_command(arguments[0], arguments[1:])
            pass
                              
      #converts expressions
        #--------------------------------------------------------------
        elif command == "expr":
            translated_cmd = ""
            i = False
            mod = False
            for j in range(len(arguments)):
              if mod:
                arguments[j-2]= "("+arguments[j-2]+"+Math.Abs("+arguments[j-2]+"*"+arguments[j]+"))"
                mod = False
              mod = (arguments[j] in ["%"])  
              if arguments[j] in ["sin","exp","sqrt","cos","tan","abs","asin","acos","atan"]:
                arguments[j] = "(double)Math."+arguments[j][0:1].upper()+arguments[j][1:]#example: converts expr asin to Math.Asin
              for type in ["double","float","int","long","char","byte","string"]:
                if arguments[j] == type:
                  arguments[j] = arguments[j].replace(type,"("+type+")")
                #remove instances that were already converted
                while arguments[j].count("(("+type+"))") > 0:
                  arguments[j] = arguments[j].replace("(("+type+"))","("+type+")")
              if arguments[j][0:1] == "$":
                  arguments[j] = arguments[j][1:]
              i = True
            pass
            for arg in arguments:
              translated_cmd += arg
        #--------------------------------------------------------------
      #convert list index (C# does not support this, so a custom method has been added with roughly the same functionality)
        elif command == "lindex" and len(arguments) == 2:
             #figure out if it is a 32 or 64 bit build and ajust return types appropriatly

             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the variable in the index [i] of the System.Array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="i"></param>   
        public static Object lindex(System.Array arr, int i)
        {
            return arr.GetValue(i);
        }
""")
             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the variable in the index [index] of the array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>   
        public static double lindex(IntPtr arr, int index)
        {
            double[] destination = new double[index + 1];
            System.Runtime.InteropServices.Marshal.Copy(arr, destination, 0, index + 1);
            return destination[index];
        }
""")
             #makes sure 64 bit versions get the correct return type
             tableReturnType = XML_Parser.look_up_method_return_type("vtkLookupTable","GetIndex")
             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the variable in the index [index] of the vtkLookupTable [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static """+tableReturnType+""" lindex(vtkLookupTable arr, double index)
        {
            return arr.GetIndex(index);
        }
""")
             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the substring ([index], [index]+1) in the string [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static int lindex(String arr, int index)
        {
           string[] str = arr.Split(new char[]{' '});      
           return System.Int32.Parse(str[index]);
        }
""")
             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the index [index] in the int array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static int lindex(int[] arr, int index)
        {
          return arr[index];
        }
""")
             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the index [index] in the float array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static float lindex(float[] arr, int index)
        {
          return arr[index];
        }
""")
             self.nontranslated_methods.append("""
        /// <summary>
        /// Returns the index [index] in the double array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static double lindex(double[] arr, int index)
        {
          return arr[index];
        }
""")
             tempVarType = self.look_up_type(arguments[0])
             if tempVarType == "vtkLookupTable":
                translated_cmd = "lindex(%s,%s)" % (arguments[0], arguments[1])
                pass
             elif tempVarType == "string":
                translated_cmd = "(int)lindex(%s,%s)" % (arguments[0], arguments[1])
                pass
             elif tempVarType != None and tempVarType.replace("[]","",1).strip() == "IntPtr":
                translated_cmd = "(double)(lindex(%s,%s))" % (arguments[0], arguments[1])
                pass
                  # elif tempVarType != None and tempVarType.replace("[]","",1).strip() == "int":
            #    translated_cmd = "(int)(lindex(%s,%s))" % (arguments[0], arguments[1]);
             elif tempVarType != None:
                translated_cmd = "("+tempVarType.replace("[]","",1)+")(lindex(%s,%s))" % (arguments[0], arguments[1])
                pass
             translated_cmd = translated_cmd.replace(";","")
             pass
        #convert sleeps
          #--------------------------------------------------------------
        elif command == "after" and len(arguments) == 1:
          translated_cmd = "System.Threading.Thread.Sleep(%s);" %(arguments[0])
          pass
        #converts string appends
          #--------------------------------------------------------------
        elif command == "append" and len(arguments) >= 2:
          translated_cmd = "%s += %s" % (arguments[0], arguments[1])
          for arg in arguments[2:]:
              translated_cmd += " + %s" % arg
              pass
          self.variable_values.AddItem(translated_cmd[:translated_cmd.find("+=")],translated_cmd[translated_cmd.find("=")+1:])
          pass
      #converts processes to methods
          #--------------------------------------------------------------
        elif command == "proc" and len(arguments) == 3:
          tempMethods = ""
          p = self._get_block_parser()
          p.update_globals(self)
          p.feed(arguments[2])
          self.update_globals(p)
          self.methods_untranslated.append([command,arguments])
          paramType = "Object"
          returnType = "Object"
          if arguments[1].strip() == "":
            paramType = ""
            pass
          if arguments[2].find("return")<0:
            returnType = "void"
            pass
          else:#searches the global variables declared already and makes the return type equal to the variable's type
            s_arg = arguments[2].split()#the split arguments
            for i in range(len(s_arg)):
              c_arg = s_arg[i]#the current argument we are looking at
              letterReg = re.compile("[a-z]")# make sure the rest of the letters in the phrase are just letters
              rest_of_return = letterReg.sub("bad_string",c_arg.replace("return","").replace(" ",""))#removes return from the statement and replaces any letters with the character bad_string
              if "return" in c_arg and "bad_string" not in rest_of_return:
                if "[" in s_arg[i+1]:
                  numRBrackets = 0
                  numLBrackets = 0
                  exp_string = ""
                  for j in range(i+1,len(s_arg)):
                    numRBrackets += s_arg[j].count("[")
                    numLBrackets += s_arg[j].count("]")
                    exp_string += s_arg[j]+" "
                    if numRBrackets==numLBrackets:
                      break
                    pass
                  exp_string = exp_string[exp_string.find("[")+1:exp_string.rfind("]")]
                  exp_string = exp_string.replace("expr","").replace("$","")
                  returnType = self.look_up_type(exp_string)
                  pass
                else:
                  returnType = self.look_up_type(s_arg[i+1])
                if returnType == None:
                  returnType = "void"
                  pass
                pass
              pass
            pass
          if arguments[0] in self.addobserver_methods:
            paramType = "vtkObject sender, vtkObjectEventArgs e"
            #because this method is called by add observer any other parameters will be passed into vtkObjectEventArgs
            if arguments[1] != "":
              arguments[1] = ""
              print "\n*************\nINFO: A procedure that was added to an event had invalad arguments\n*************"
              pass
          tempMethods += "\n"+self.indent+"/// <summary>\n"+self.indent+"///A procedure translated from the tcl scripts\n"+self.indent+"/// </summary>\n"+self.indent+"public static %s %s (%s " % (returnType, arguments[0], paramType)
          proc_args = arguments[1].split()
          i = False
          pair = 0
          for pa in proc_args:
            if pa.strip() == "{":
              pair = 1
              continue
            elif pa.strip() == "}":
              pair = 0
              continue
            if i and pair != 2:
              tempMethods += ", Object "
              pass
            if pair == 2:
              tempMethods += "="
              pass
            if pair == 1:
              pair = 2
              pass
            i = True
            tempMethods += pa
            pass
          tempMethods +=")\n"+self.indent+"  {\n"
          tempMethods += p.output+self.indent+"  }\n"
          self._procedure_list.append(arguments[0])
          self.methods.append(tempMethods)
          translated_cmd = "//method moved"
          pass
        #converts set command.
          #--------------------------------------------------------------
        elif command == "set" and len(arguments) == 2:
          if arguments[0] == "diagonal":
              pass
          #makes sure this variable name is not the same as a method name
          if arguments[0] == self.file_name:
            arguments[0] +="_var";
            pass
          tempType = self.look_up_type(arguments[1])
          if arguments[1].count("{")>0:
            args = (arguments[1].replace("{","")).replace("}","").split()
            tempType = ""
            arguments[i] = ""
            for i in range(len(args)):
              if args[i].count("\"")<2:
                args[i] = args[i].replace("$","")
                pass
              tempType2 = self.look_up_type(args[i])
              tempType = [tempType,tempType2][typeValues.GetItem(tempType) < typeValues.GetItem(tempType2)]
              arguments[1] += args[i]+","
              pass
            tempType = tempType+"[]"
            arguments[1] = "new "+tempType+"{"+arguments[1][:-1]+"}"
            pass
          typeCast = ""
          #hack that changes the culler into its only derived class so it can call methods
          if tempType == "vtkCuller":
            tempType = "vtkFrustumCoverageCuller"
            typeCast = "(vtkFrustumCoverageCuller)"
            pass
          tempGlobal = "static "+str(tempType)+" "+str(arguments[0])
          self.global_variables.append(tempGlobal+";\n")
          self.update_globals(self)
          if arguments[1]!="" and arguments[1].split()[0]!="parmsArr":
            translated_cmd = "%s = %s%s;" % (arguments[0],typeCast, arguments[1]) 
            self.variable_values.AddItem(arguments[0],arguments[1])
            pass
          else:
            self.variable_values.AddItem(arguments[0],arguments[1][arguments[1].find("methodInfo.Invoke"):])
            translated_cmd = arguments[1].replace("methodInfo.Invoke",arguments[0]+" = methods_info.Invoke")
            pass
          pass

        #converts foreach loops
          #--------------------------------------------------------------
        elif command == "foreach" and len(arguments) == 3:
          p = self._get_block_parser()
          p.update_globals(self)
          p.indent = self.indent + "    "
          p.feed(arguments[2])
          argType = self.look_up_type(arguments[1])
          if argType == "string":
            val = self.variable_values.GetItem(arguments[1])
            if val != None:
              val = val[1:-1]
              val = self.look_up_type(val.strip()[0])
              if val == "Object":
                val = "string"
                pass
              arguments[1] = arguments[1]+".Split(new char[]{' '})"
              if val == "int":
                p.output = p.output.replace(arguments[0],"Int32.Parse("+arguments[0]+")")
                pass
              elif val == "long":
                p.output = p.output.replace(arguments[0],"Int64.Parse("+arguments[0]+")")
              elif val in ["double"]:
                p.output = p.output.replace(arguments[0],val[0:1].upper()+val[1:]+".Parse("+arguments[0]+")")
                pass
              pass
            pass
          translated_cmd = "foreach (%s %s in %s)\n%s{\n" % (argType, arguments[0], arguments[1],self.indent)
          translated_cmd += p.output
          self.update_globals(p)
          translated_cmd += "\n" + self.indent + "    }"
          pass
        #converts for loops
          #--------------------------------------------------------------
        elif command == "for" and len(arguments) == 4:
            p = self._get_block_parser()
            p.update_globals(self)
            p.feed(arguments[0])
            translated_cmd = p.output.strip() + "\n"
            self.update_globals(p)
            p = self._get_block_parser()
            p.update_globals(self)
            p.feed(arguments[1])
            translated_cmd += self.indent + "while(" + p.output.strip().replace(";","") + ")\n"+self.indent+"  {\n"
            self.update_globals(p)
            p = self._get_block_parser()
            p.update_globals(self)
            p.indent = self.indent + "    "
            p.feed(arguments[3])
            translated_cmd += p.output
            self.update_globals(p)
            p = self._get_block_parser()
            p.update_globals(self)
            p.indent = self.indent + "    " 
            p.feed(arguments[2])
            self.update_globals(p)
            translated_cmd += p.output+p.indent[3:]+" }\n"
        #converts while loops
        #--------------------------------------------------------------
        elif command == "while" and len(arguments) == 2:
            p = self._get_block_parser()
            p.update_globals(self)
            p.feed(arguments[0])
            translated_cmd = "while ("+(p.output.strip())+")\n"+self.indent+"  {\n" 
            self.update_globals(p)
            p = self._get_block_parser()
            p.update_globals(self)
            p.indent = self.indent + "    "
            p.feed(arguments[1])
            translated_cmd += p.output
            self.update_globals(p)
            translated_cmd += p.indent[3:]+" }"
        
        #convert try catch blocks
        #--------------------------------------------------------------
        elif command == "catch" and len(arguments) == 1:
            p = self._get_block_parser()
            p.update_globals(self)
            p.indent = self.indent
            p.feed(arguments[0])
            self.update_globals(p)
            translated_cmd = "try\n"+self.indent+"{\n"+self.indent+"   "+p.output.strip()+"\n"+self.indent+"}"
            if self.parent_command!="if":
              translated_cmd += "\n"+self.indent+"catch(Exception e){Console.WriteLine(e);}"

        #converts if and else if
        #--------------------------------------------------------------
        elif command in ["if", "elseif"] and len(arguments) >= 2: 
            p = self._get_block_parser()
            p.parent_command = "if"
            p.update_globals(self)
            p.indent = self.indent
            p.feed(arguments[0])
            if arguments[0].split()[0] == "[catch":
                translated_cmd = p.output.strip()+self.indent
                temp = translated_cmd[translated_cmd.rfind("}")+1:]
                temp = temp.split()
                if len(temp) >1 and ((temp[0] == "!=" and temp[1] == "0") or (temp[0] == "==" and temp[1] == "1")):
                    translated_cmd = translated_cmd[:translated_cmd.rfind("}")+1]+"\n"+self.indent+"catch(Exception)\n"+self.indent+"{\n"
                elif len(temp)>1 and ((temp[0] == "!=" and temp[1] == "1") or (temp[0] == "==" and temp[1] == "0")):
                    self.global_variables.append("static string tryCatchError;\n")
                    translated_cmd = translated_cmd[:translated_cmd.rfind("}")]+self.indent+"  tryCatchError = \"NOERROR\";\n"+self.indent+"}\n"+self.indent+"catch(Exception)\n"+self.indent+"{tryCatchError = \"ERROR\";}\n"+self.indent+"\nif(tryCatchError.Equals(\"NOERROR\"))\n"+self.indent+"{\n"
                else:
                    translated_cmd == "case not supported yet, please add to the python script"
            elif command == "if":
                translated_cmd    = "if ("+p.output.strip().replace(";","")+")\n"+self.indent+"  {\n"  
            else:
                translated_cmd    = "else if ("+p.output.strip().replace(";","")+")\n"+self.indent+"  {\n"  
            self.update_globals(p)
            p = self._get_block_parser()
            p.update_globals(self)
            p.indent    = self.indent + "    "
            p.feed(arguments[1])
            translated_cmd += p.output+self.indent+"  }\n"
            self.update_globals(p)
            if len(arguments) > 2:
                translated_cmd += self.indent + \
                    self.translate_command(arguments[2], arguments[3:])
            translated_cmd += "\n"+self.indent + ""
        #converts else
        #--------------------------------------------------------------
        elif command=="else" and len(arguments)==1:
            translated_cmd = "else\n"+self.indent+"  {\n"
            p = self._get_block_parser()
            p.update_globals(self)
            p.indent = self.indent + "    "
            p.feed(arguments[0])
            self.update_globals(p)
            translated_cmd += p.output+self.indent+"  }"
        #converts return
        #--------------------------------------------------------------
        elif command == "return" and len(arguments) == 0:
            translated_cmd = "return;" 
        #converts return
        #--------------------------------------------------------------
        elif command == "return" and len(arguments) == 1:
            translated_cmd = "return %s;" % arguments[0]
        #converts open
        #--------------------------------------------------------------
        elif command == "open" and len(arguments) >= 1:
            if arguments[0].count("\"")<2:
              arguments[0]= "\""+arguments[0]+"\""
            if arguments[1] in ["\"w\"","w"]:
              translated_cmd = "new StreamWriter(%s)" % arguments[0]
            elif arguments[1] == ["\"r\"","r"]:
              translated_cmd = "new StreamReader(%s)" % arguments[0]
            self.using_statements.append("using System.IO;\n")
        #converts close
        #--------------------------------------------------------------
        elif command == "close" and len(arguments) == 1:
            translated_cmd = arguments[0]+".Close();"
        #converts gets only for a TextReader
        #--------------------------------------------------------------
        elif command == "gets" and len(arguments) == 1:
            translated_cmd = "text_reader.ReadLine();" 
        #converts gets only for a TextReader
        #--------------------------------------------------------------
        elif command == "gets" and len(arguments) == 2:
            self.global_variables.append("static string "+arguments[1]+";\n")
            self.update_globals(self)
            self.nontranslated_methods.append("""
   ///<summary> TCL to CS string reader</summary>
   public static int TclToCSStringReader(String s)
     {
       if(s==null)
       {
       return -1;
       }
       return s.Length;
     }
""")
            translated_cmd ="TclToCSStringReader("+arguments[1]+" = ((StreamReader)"+arguments[0]+").ReadLine())" 
        #--------------------------------------------------------------
        elif command == "scan" and len(arguments) > 1:
          i = 2
          translated_cmd =""
          self.using_statements.append("using System.IO;\n")
          tempType = ""
          while i<len(arguments):
            if (arguments[1].split()[i-2]).replace("\"","").replace("%","").strip() == "s" or (arguments[1].split()[i-2]).replace("\"","").replace("%","").strip() == "c":
              self.global_variables.append("static string "+arguments[i]+";\n")
              self.update_globals(self)
              tempType = "(string)"
              pass
            elif (arguments[1].split()[i-2]).replace("\"","").replace("%","").strip() == "f" or (arguments[1].split()[i-2]).replace("\"","").replace("%","").strip() == "lf":
              self.global_variables.append("static double "+arguments[i]+";\n")
              self.update_globals(self)
              tempType = "Double.Parse"
              pass
            elif (arguments[1].split()[i-2]).replace("\"","").replace("%","").strip() == "d":
              self.global_variables.append("static int "+arguments[i]+";\n")
              self.update_globals(self)
              tempType = "Int.Parse"
              pass
            translated_cmd += str(arguments[i])+" = "+(tempType+"("+str(arguments[0])+\
                ".Substring("+str(i-2)+","+str(i-1)+"))").replace(";","")+";\n"+self.indent
            self.variable_values.AddItem(arguments[i],(str(arguments[0])+".Substring("+str(i-2)+","+str(i-1)+"))").replace(";",""))
            i=i+1
            pass
          pass
        #converts incriments
        #--------------------------------------------------------------
        elif command == "incr" and len(arguments) == 1:
            translated_cmd = "%s = %s + 1;" % (arguments[0], arguments[0])
            self.variable_values.AddItem(arguments[0],arguments[0]+"+1")
        #converts incriments
        #--------------------------------------------------------------
        elif command == "incr" and len(arguments) == 2:
            translated_cmd = "%s = %s + %s;" % (arguments[0], arguments[0], arguments[1])
            self.variable_values.AddItem(arguments[0],arguments[0]+"+"+arguments[1])
        #there is no tcl interpreter, skip it
        #--------------------------------------------------------------
        elif command == "info" and len(arguments) > 0:
          
          if(len(arguments)==2):
            if arguments[0] == "command":
              translated_cmd = arguments[1]
            if arguments[0] == "commands":
              translated_cmd = "System.Type.GetType(source).GetProperty("+arguments[1]+")"
              pass
            pass
          else:
              translated_cmd ="true/*skipping info:"+str(arguments)+"*/"
        #there is no tcl platform, skip it
        #--------------------------------------------------------------
        elif command == "tcl_platform" and len(arguments) >= 3:
            if arguments[1]=="platform":
              arguments[1] = "current_platform"
              self.global_variables.append("static string current_platform = \"win32\";\n")
              translated_cmd = ""
              for arg in arguments:
                translated_cmd += arg+" "
            else:
              translated_cmd = "//skipping "+command+" "+str(arguments)
            #converts declarations
        #--------------------------------------------------------------
        elif command in self.class_list:
            #translate a VTK object create command.
            if len(arguments) == 1:
                count = 0
                if arguments[0] == self.file_name:
                  arguments[0] +="_var";
                #counts the number of dimensions if it is an array
                if arguments[0].count("[")>0:
                  count = arguments[0][arguments[0].find("["):arguments[0].rfind("]")].count(",")+1
                if command in self.abstract_classes:
                   # translated_cmd = "%s = (%s) vtkGraphicsFactory.CreateInstance(\"%s\");" % (arguments[0],command, command)
                   translated_cmd = "%s = %s.New();" % (arguments[0],command)
                elif command == "vtkCommand":
                   translated_cmd = "//cannont translated vtkCommand because it is abstract"
                else:
                    translated_cmd = "%s = %s.New();"% (arguments[0],command)
                if count>0:
                    arguments[0] = arguments[0][:arguments[0].find("[")]
                    arguments[0] += " = new "+command
                i = 0
                #allocates memory for arrays
                if count>0:
                   command +="["
                   arguments[0]+="[100"
                   i = 1
                   while i<count:
                      command+=","
                      arguments[0]+=",100"
                      i=i+1
                   command += "]"
                   arguments[0] += "]"
                self.global_variables.append("static "+command+" "+arguments[0]+";\n")
                self.update_globals(self)
                self.variable_values.AddItem(translated_cmd[:translated_cmd.find("=")],translated_cmd[translated_cmd.find("=")+1:])
            else:
                self._error("Invalid command: %s %s" % (command, `arguments`))
        #TODO this should be able to be translated
        #--------------------------------------------------------------
        elif command == "BuildBackdrop" and len(arguments) == 7:
          #translated_cmd = "[base, back, left] = BuildBackdrop(%s" % arguments[0]
          #for arg in arguments[1:]:
          #    translated_cmd += ", %s" % arg
          #translated_cmd += ")"
          self.global_variables.append("static vtkCubeSource basePlane;\n")
          self.global_variables.append("static vtkPolyDataMapper baseMapper;\n")
          self.global_variables.append("static vtkActor base;\n")
          self.global_variables.append("static vtkCubeSource backPlane;\n")
          self.global_variables.append("static vtkPolyDataMapper backMapper;\n")
          self.global_variables.append("static vtkActor back;\n")
          self.global_variables.append("static vtkCubeSource leftPlane;\n")
          self.global_variables.append("static vtkPolyDataMapper leftMapper;\n")
          self.global_variables.append("static vtkActor left;\n")
          
          self.nontranslated_methods.append("""
  /// <summary>
  ///A procedure translated from the tcl scripts
  /// </summary>
  public static void BuildBackdrop (double minX, double maxX, double minY, double maxY, double minZ, double maxZ, double thickness)
    {
      if ((basePlane) == null)
        {
          basePlane = vtkCubeSource.New();
        }

      
      basePlane.SetCenter((double)(maxX+minX)/2.0,(double)minY,(double)(maxZ+minZ)/2.0);
      basePlane.SetXLength((double)(maxX-minX));
      basePlane.SetYLength((double)thickness);
      basePlane.SetZLength((double)(maxZ-minZ));
      if ((baseMapper) == null)
        {
          baseMapper = vtkPolyDataMapper.New();
        }

      
      baseMapper.SetInput((vtkPolyData)basePlane.GetOutput());
      if ((base) == null)
        {
          base = vtkActor.New();
        }

      
      base.SetMapper((vtkMapper)baseMapper);
      if ((backPlane) == null)
        {
          backPlane = vtkCubeSource.New();
        }

      
      backPlane.SetCenter((double)(maxX+minX)/2.0,(double)(maxY+minY)/2.0,(double)minZ);
      backPlane.SetXLength((double)(maxX-minX));
      backPlane.SetYLength((double)(maxY-minY));
      backPlane.SetZLength((double)thickness);
      if ((backMapper) == null)
        {
          backMapper = vtkPolyDataMapper.New();
        }

      
      backMapper.SetInput((vtkPolyData)backPlane.GetOutput());
      if ((back) == null)
        {
          back = vtkActor.New();
        }

      
      back.SetMapper((vtkMapper)backMapper);
      if ((leftPlane) == null)
        {
          leftPlane = vtkCubeSource.New();
        }

      
      leftPlane.SetCenter((double)minX,(double)(maxY+minY)/2.0,(double)(maxZ+minZ)/2.0);
      leftPlane.SetXLength((double)thickness);
      leftPlane.SetYLength((double)(maxY-minY));
      leftPlane.SetZLength((double)(maxZ-minZ));
      if ((leftMapper) == null)
        {
          leftMapper = vtkPolyDataMapper.New();
        }

      
      leftMapper.SetInput((vtkPolyData)leftPlane.GetOutput());
      if ((left) == null)
        {
          left = vtkActor.New();
        }

      
      left.SetMapper((vtkMapper)leftMapper);
    }
    """)
          translated_cmd = command+"("
          for a in arguments:
            translated_cmd+=a+", "
            pass
          translated_cmd = translated_cmd[:-2]+");"
          pass
        #TODO figure out what this does and translate it
        #--------------------------------------------------------------
        elif command in ["case1", "case2", "case3", "case4", "case5", "case6", "case7", 
          "case8", "case9", "case10", "case11", "case12", "case13", "case14"] and \
              len(arguments) == 3:
          translated_cmd = "//case skipped not supported"+command+" "+str(arguments)
          pass
        #converts truth operators
        #--------------------------------------------------------------
        elif len(arguments) >= 2 and arguments[0] != "" and arguments[0][0] in ["<",">","=","!"]:
          if "try" in command.split():
            translated_cmd = command
            pass
          else:
            translated_cmd = "("+command+")" 
            pass
          for arg in arguments:
            if arg == "\"\"" and self.look_up_type(command)!="string":
              arg = "null"
            translated_cmd+=    " " + arg
            pass
          pass
        #do not use file
        #--------------------------------------------------------------
        elif command == "file" and len(arguments) > 0:
          #TODO there are most likeley more file commands that can be translated easily
          #It might be a good idea to put them in here
          if arguments[len(arguments)-1].count("\"")<2:
            arguments[len(arguments)-1]= "\""+arguments[len(arguments)-1]+"\""
            pass
          if "delete" in arguments:
            translated_cmd = "File.Delete("+arguments[len(arguments)-1]+");"
            self.using_statements.append("using System.IO;\n")
            pass
          else:
            translated_cmd = "//file command skipped not supported "+command+" "+str(arguments)
            pass
          pass
        #converts Unregister
        #--------------------------------------------------------------
        elif len(arguments) > 0 and arguments[0] == "UnRegister":
          #translated_cmd = "%s.UnRegister(" % command
          #if len(arguments) < 2 or len(arguments[1].strip()) == 0:
          #    translated_cmd += "None"
          #else:
          #    translated_cmd += "%s" % arguments[1]
          #translated_cmd += ");"
          translated_cmd = "//"+command+".Unregister("+str(arguments)+") Skipped"
          pass
        #converts all commands in the procedure list
        #--------------------------------------------------------------
        elif command in self._procedure_list:
          translated_cmd = "%s(" % command
          i = False
          for arg in arguments:
            if i:
              translated_cmd +=","
              pass
            i = True
            translated_cmd += arg
            pass
          translated_cmd =translated_cmd.replace(";","")+");"
          pass
        #converts source
        #--------------------------------------------------------------
        elif command == "source":
          #self.global_variables.append("static string source;\n")
          #translated_cmd = "source = "+arguments[0]
          translated_cmd = "//skipping source"
          pass
        #converts delete commands
        #--------------------------------------------------------------
        elif len(arguments) == 1 and arguments[0].strip() == "Delete":
          translated_cmd = "//skipping Delete %s" % command
          pass
        #converts exit
        #--------------------------------------------------------------
        elif command == "exit":
          translated_cmd = "//exit not translated"
          pass
        #converts vtk object commands
        #--------------------------------------------------------------
        #this is an absurdly long if block that converts commands like (somevtkobject).VTKObjectMethod(arg(0), arg(1), ..., arg(n-1))
        elif len(arguments) > 0:
          translated_cmd = ""
          if arguments[0].strip() == "Print" :
            translated_cmd = "//"
            pass
          #translate a VTK object command invoation.
          if command[len(command)-1:]==";" :
            command = command[:len(command)-1]
            pass
          for i in range(0,len(arguments)):
            #arguments[i] = arguments[i].replace("VTK_DATA_ROOT","System.Environment.GetEnvironmentVariable(\"VTK_DATA_ROOT\")")
            arguments[i] = arguments[i].replace(";","")
            if arguments[i] in vtkCommandList.get_vtk_event_ids():
              arguments[i] = "(uint)(vtkCommand.EventIds."+arguments[i]+")"
              pass
            for j in range(len(vtk_colors.colors)):
              if arguments[i] == vtk_colors.colors[j] and j%2 == 0:  
                arguments[i] = vtk_colors.colors[(j+1)]
                pass
              pass
            pass
          for i in range(0,len(self.global_variables)):
            #funky way of calling a method without knowing the class type
            if self.global_variables[i].split()[1] == "Object" and self.global_variables[i].split()[2][:-1] == command:
              self.global_variables.append("static Object[] parmsArr;\n")
              self.global_variables.append("static System.Reflection.MethodInfo methodInfo;\n")
              self.update_globals(self)
              method_name = str(arguments[0])
              i = 1
              params_value = ""
              while i<len(arguments):
                params_value += arguments[i]+","
                i = i+1
                pass
              params_value = params_value[:-1]#chop off the last comma that would cause syntax error
              if params_value!="":
                params_count = params_value.count(",")+1;
                translated_cmd += "parmsArr = new object[%s] {%s};\n" %(params_count, params_value)
                pass
              else:
                params_count = 0
                translated_cmd += "parmsArr = null;\n"
                pass
              tempValue = self.variable_values.GetItem(command)

              tempValue = self.remove_arguments(tempValue)

              translated_cmd += self.indent+"methodInfo = "+tempValue[:tempValue.find(".")]+".GetType()"

              tempValue = tempValue[tempValue.find(".")+1:]
              for i in range(tempValue.count(".")+1):
                translated_cmd += ".GetMethod(\"%s\").ReturnType" %(tempValue[:tempValue.find("(")])
                tempValue = tempValue[tempValue.find(")")+1:]
                pass
              translated_cmd+=".GetMethod(\"%s\");\n" % arguments[0]
              translated_cmd+=self.indent+"methodInfo.Invoke("+command+",parmsArr);\n"
              return translated_cmd #don't do the other part function
            pass
          if command == self.file_name:
            command+="_var"
            pass
          translated_cmd += "%s.%s(" % (command, arguments[0])
          #special case because TCL creates an instance of math and C# uses the static method
          command_type = self.look_up_variable_type(command)
          if command_type == None:
            command_type = self.look_up_type(command)  
            pass
          if arguments[0] in ["Random"]:
            translated_cmd = "vtkMath."+arguments[0]+"("
            pass
          #add observer is obsolete in C#, so we use Events instead
          elif arguments[0] == "AddObserver":
            base_name = arguments[2]
            var_name = arguments[2]
            #parses the name of the event from the event id stuff
            event_name = arguments[1][arguments[1].rfind(".")+1:arguments[1].rfind(")")].replace("Event","Evt").replace("\"","")
            translated_cmd = command+"."+event_name+" += new vtkObject.vtkObjectEventHandler("+arguments[2]+");"
            self.addobserver_methods.append(base_name)
            return translated_cmd
          elif XML_Parser.is_static_method(command_type,arguments[0]):
             translated_cmd = command_type+"."+arguments[0]+"("
          #this part creates a list of all overloaded methods from XML files and
          #attempts to figure out which one would be the best to cast everything as
          list = XML_Parser.get_params_type(command_type,arguments[0])
          known_args = arguments[1:]
          # remove all items from the list that do not have the same number of args
          #as the number of params being passed in
          i = 0
          temp_list = list
          list = []
          
          
          if temp_list != None:
            while i<len(temp_list):
              if len(temp_list[i]) == len(known_args):
                list.append(temp_list[i])
                pass
              i= i+1
              pass
            #if there more than one left keep checking
            if len(list)>1:
              for i in range(len(known_args)):
                known_args[i] = [self.look_up_type(known_args[i])]
                if known_args[i][0] == None:
                  print "\n*************\nWARNING: A method called had no type in the xml files, replaced with Object\n*************"
                  known_args[i] = ["Object"]
                if known_args[i][0] in ["double","float"]:
                  known_args[i] = ["double","float"]
                  pass
                pass
              #look for a list with the same arguments that you have types
              for temp_list in list:
                works = False
                for i in range(len(temp_list)):
                  if temp_list[i] in known_args[i] or known_args[i][0] == "Object":
                    works = True
                    pass
                  else:
                    if known_args[i][0] in class_list and temp_list[i] in class_list:
                      temp_list_var = temp_list[i]
                      temp_known_var = known_args[i][0]
                      found = False
                      while(temp_list_var!="vtkObject"):
                        if  temp_list_var==temp_known_var:
                          found=True
                          works = True
                          temp_list[i] = temp_list_var
                          break
                        temp_list_var = XML_Parser.superclass(temp_list_var)
                        pass
                      if found == False:
                        temp_list_var = temp_list[i]
                        while(temp_known_var!="vtkObject"):
                          if temp_list_var == temp_known_var:
                            found = True
                            works = True
                            temp_list[i] = temp_known_var
                            break
                          temp_known_var = XML_Parser.superclass(temp_known_var)
                          pass
                        pass
                      if found == False:
                        works = False
                        break
                    elif known_args[i][0].count("_Command")==1:
                      works = True
                      pass
                    else:
                      works = False
                      break
                    pass
                  pass
                if works == True:
                  list = []
                  list.append(temp_list)
                  break
                pass
              #if it can't find a match it will type cast it for you
              if len(list)>1:
                temp_var = list[0]
                list = []
                list.append(temp_var)
                pass
              pass
            pass
          else:
            list = None
            pass
         
          if "/" in arguments:
            translated_cmd+="\""
            i = 1
            while i<len(arguments):
              if arguments[i] in self.variable_values.keys:
                translated_cmd+="\"+"+arguments[i]+"+\""
                pass
              else:
                translated_cmd+=arguments[i]
                pass
              i = i+1
              pass
            translated_cmd+="\""
            pass
          else:
            #if len(arguments) > 9: #typically, methods taking more than 9 args in VTK take them as lists.
            #  translated_cmd += "new double[]{"
            #   pass
            for i in range(1,len(arguments)):
              if i > 1:
                translated_cmd += ","
                pass
              if arguments[i] != "" :
                if list!= None and len(list) >0 and len(list[0])+1 == len(arguments):
                  #typecast the args
                  translated_cmd += "("+list[0][i-1]+")"
                  pass
                if arguments[i]==self.file_name:
                  arguments[i]+="_var"
                  pass
                translated_cmd += arguments[i]
                pass
              else:
                translated_cmd += "null"
                pass
              pass
           # if len(arguments) > 9:
           #   translated_cmd += "}"
           #   pass
            pass
          #check the hints file to see if it is a double[] trying to get a refdouble
          if len(arguments)==2 and arguments[0] in self.refdouble_hints:
            arg_type = str(self.look_up_type(arguments[1]))
            if "[]" in arg_type:
              cmd_index = self.refdouble_hints.index(arguments[0])
              translated_cmd = command+"."+arguments[0]+"("
              for i in range(0,int(self.refdouble_hints[cmd_index+1])):
                translated_cmd += arguments[1]+"["+str(i)+"],"
                pass
              translated_cmd=translated_cmd[:-1]
              pass
          translated_cmd += ");"   
          pass 
        #converts regular function calls
        #--------------------------------------------------------------
        elif command and len(arguments) == 0:
          if self.variable_values.GetItem(command)!=None:
            return command+"!=0"#if a variable is a command and there are no arguments, chances are you are in an if statement
          if command == "1":
            return "true"
          if command == "0":
            return "false"
          for method in self.methods:
            method = method[:method.find("(")]
            method = method[method.rfind(" ")+1:]
            if command == method:
              return command+"();"
          return command
        #double dots in commands indicate a tk window
        if ".." in re.compile("\".*?\"").sub("",translated_cmd):
          return "//tk window skipped.."
        return translated_cmd
        pass

    # takes a string in form spam.Eggs(foo.bar())
    # returns string in form spam.Eggs()
    # list is an out param that if specified will return ['spam.Eggs(','foo.bar()',')']
    def remove_arguments(self, expr,list = []):
      if expr.count("(")<1:
        return expr
      tempVar = expr[expr.find("(")+1:]
      exprWithoutArgs = expr[:expr.find("(")+1]
      removedArr = ""
      #loops through removeing any closed parens
      while not(tempVar.count(")") == tempVar.count("(")):
        removedArr += tempVar[:tempVar.find(")")+1]
        tempVar = tempVar[tempVar.find(")")+1:]
      #append to the list
      list.append(exprWithoutArgs)
      list.append(removedArr)
      list.append(")")
      #the extra rparen is removed so put it back in
      exprWithoutArgs += ")"
      #recurse with the remaining args
      return exprWithoutArgs+self.remove_arguments(tempVar,list)

    def handle_command(self, translated_cmd):
        translated_cmd = self.indent + translated_cmd + "\n"
        self.output += translated_cmd
        pass

if __name__ == "__main__":
    input_file = None
    output_file = None
    class_file = None
    prefix_file = None
    convert_file_list_file = None
    namespace = "vtk"
    touch_file = None
    kit_files_dir = ""
    for i in range(0, len(sys.argv)):
        if sys.argv[i] == "-i" and i < len(sys.argv)-1:
            input_file = sys.argv[i+1]
        if sys.argv[i] == "-o" and i < len(sys.argv)-1:
            output_file = sys.argv[i+1]
        if sys.argv[i] == "-f" and i < len(sys.argv)-1:
            class_file = sys.argv[i+1]
        if sys.argv[i] == "-n" and i < len(sys.argv)-1:
            namespace = sys.argv[i+1]
        if sys.argv[i] == "-p" and i < len(sys.argv)-1:
            prefix_file = sys.argv[i+1]
        if sys.argv[i] == "-l" and i < len(sys.argv)-1:
            convert_file_list_file = sys.argv[i+1]
        if sys.argv[i] == "-t" and i < len(sys.argv)-1:
            touch_file = sys.argv[i+1]
        if sys.argv[i] == "-xmldir" and i < len(sys.argv)-1:
            XML_Parser.xml_dir = sys.argv[i+1]
        if sys.argv[i] == "-k" and i < len(sys.argv)-1:
            kit_files_dir = sys.argv[i+1]

    if (not input_file or not output_file) and not convert_file_list_file:
        print "Usage: %s  [-o <output tcl test> -i <input tcl test>]"\
                "[-f <class name list>] [-n <namespace>] [-p <prefix file>]"\
                "[-l <semi-colon separated list of tcl tests to convert>]"\
                "[-t <file to touch on conversion complete>]"\
                "[-xmldir <directory where the vtk xml documents are kept>]"\
                "[-k <vtk*Kit.cmake file path>] [-k ...] ..." % sys.argv[0]
        print "Got Args: %s" % `sys.argv`
        sys.exit(1)
    class_list = []
    if class_file:
        try:
            fp = file(class_file, "r")
            new_class_list = fp.readlines()
            for a in new_class_list:
               class_list.append(string.strip(a))
            fp.close()
        except:
            print "Failed to read class list file %s" % class_file
            sys.exit(1)
    else:
        try:
            import vtkClassList
            class_list = vtkClassList.get_vtk_classes()
            pass
        except:
            print "ERROR: Failed to load module vtkClassList."
            sys.exit(1)
     
    if not class_list:
        print "Cannot find list of classes. Please provide -f <file> option"
        sys.exit(1)
  
    prefix_content = ""
    try:
        fp = file(prefix_file, "r")
        prefix_content = fp.read()
        fp.close()
    except:
        pass
    convert_file_list = []
    output_file_list = []
    if convert_file_list_file:
        try:
            fp = file(convert_file_list_file, "r")
            filename_list = fp.read().split(";")
            fp.close()
            for i in range(0, len(filename_list), 2):
                convert_file_list.append(filename_list[i])
                output_file_list.append(filename_list[i+1])
        except:
            print "Failed to read list of file to translate %s" % convert_file_list_file
            print "%s" % sys.exc_info()[1]
            sys.exit(1)
    
    if input_file:
        convert_file_list.append(input_file)
    if output_file:
        output_file_list.append(output_file)
   
    for i in range(0,len(convert_file_list)):
        data = ""
        ip_filename = convert_file_list[i].strip()
        op_filename = output_file_list[i].strip()
        try:
            print "Converting %s" % ip_filename
            fp = file(ip_filename, "r")
            data = fp.read()
            fp.close()
        except:
            print "Failed to read input file %s" % ip_filename 
            sys.exit(1)

        p = vtkTclToCsConvertor()
        p.class_list = class_list
        p.name_space = namespace
        p.print_header(prefix_content)
       
        p.feed(data)
        p.print_footer()
        p.post_process()
        if p.success() or True:
            try:
                ofp = file(op_filename, "w")
                ofp.write(p.output)
                ofp.close()
            except:
                print "Failed to write output file %s" % op_filename
                sys.exit(1)
        else:
            print "Conversion failed!"
            sys.exit(1)
    if touch_file:
        try:
            ofp = file(touch_file, "w")
            ofp.write("Done\n")
            ofp.close()
        except:
            print "Failed to touch file %s" % touch_file
            sys.exit(1)
    sys.exit(0) 






















