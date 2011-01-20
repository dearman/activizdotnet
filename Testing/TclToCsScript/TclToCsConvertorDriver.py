import glob
import os
import time
import recursiveglob
import shutil
import sys

VTK_SOURCE = "C:\\VTK"
XML_DIR = "C:\\Activiz Build\\xml"
REFERENCES_DIR = "C:\\Activiz\\trunk\\ActivizDotNet\\Testing\\TclToCsScript"
PYTHON_PATH = "python.exe"
CSHARP_PATH = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v3.5\\csc.exe"
CMAKE_PATH = "cmake.exe"
CONVERTER_PATH = os.getcwd()+"\\vtkTclToCsConvertor.py"
TESTDRIVER_PATH = os.getcwd()+"\\csharptestdriver.cs"

testsToConvert = "ALL"
if __name__ == "__main__":
  #parse command line
  for i in range(0, len(sys.argv)):
    if sys.argv[i] == "-vtkdir" and i < len(sys.argv)-1:
        VTK_SOURCE = sys.argv[i+1]
    if sys.argv[i] == "-xmldir" and i < len(sys.argv)-1:
        XML_DIR = sys.argv[i+1]
    if sys.argv[i] == "-refdir" and i < len(sys.argv)-1:
        REFERENCES_DIR = sys.argv[i+1]
    if sys.argv[i] == "-pythondir" and i < len(sys.argv)-1:
        PYTHON_PATH = sys.argv[i+1]
    if sys.argv[i] == "-csharpdir" and i < len(sys.argv)-1:
        CSHARP_PATH = sys.argv[i+1]
    if sys.argv[i] == "-cmakedir" and i < len(sys.argv)-1:
        CMAKE_PATH = sys.argv[i+1]
    if sys.argv[i] == "-converterdir" and i < len(sys.argv)-1:
        CMAKE_PATH = sys.argv[i+1]
    if sys.argv[i] == "-testdriverdir" and i < len(sys.argv)-1:
        CMAKE_PATH = sys.argv[i+1]
    if sys.argv[i] == "-teststoconvert" and i < len(sys.argv)-1:
        fp = file(sys.argv[i+1],"r")
        testsToConvert = fp.read()
        fp.close()
    if sys.argv[i] == "-help" and i < len(sys.argv):
        print """
        usage = 
        python driver.py [optional: [-vtkdir ]|[-xmldir ]|[-refdir ]|[-pythondir ]|[-csharpdir ]|[-cmakedir ]|[-teststoconvert ]]
        
        Defaults
        vtkdir = """+VTK_SOURCE+"""
        xmldir = """+XML_DIR+"""
        refdir = """+REFERENCES_DIR+"""
        pythondir = """+PYTHON_PATH+"""
        csharpdir = """+CSHARP_PATH+"""
        cmakedir = """+CMAKE_PATH+"""
        converterdir = """+CONVERTER_PATH+"""
        testdriverdir = """+TESTDRIVER_PATH+"""
        teststoconvert = """+testsToConvert+"""
  """
        quit();
    pass
  t = time.localtime()
  DATE = "Converted CSharp Tests "+str(t[0])+"-"+str(t[1])+"-"+str(t[2])+"_"+str(t[3])+"."+str(t[4])+"."+str(t[5])
  os.mkdir(DATE)
  os.mkdir(DATE+"\\Compiling")
  os.mkdir(DATE+"\\NotCompiling")

  allTests = ""
  compilingTests = ""
  nonCompilingTests = ""
  translationStats = ""
  nonTranslatableTests = ""
  big_batch = ""
  little_batch = ""

  print "\n---Generating List To Convert\n"


  glob_result = recursiveglob.rec_glob(VTK_SOURCE,"*.tcl")
  #glob_result = ["C:\\VTK\\Wrapping\\Tcl\\vtktesting\\colors.tcl"]
  progress = 0
  compilingTestsNum = 0
  nonCompilingTestsNum = 0
  nonTranslatableTestsNum = 0
  totalTestsNum = len(glob_result)

  #loop through all the TCL Tests
  for v in sorted(glob_result):
    #Get the name without the extension
    index = [v.rfind('\\'),v.rfind('/')][v.rfind('\\')<v.rfind('/')]
    v_out = "AV"+(v[(index+1):(v.rfind("."))])
    if testsToConvert != "ALL":
      if v_out[2:] not in testsToConvert:
        continue
      else:
        totalTestsNum = testsToConvert.count("\n")
        pass
      pass
    progress+=1
    print str(progress)+"/"+str(totalTestsNum)+"\n"
    allTests += v+"\n"
    print "------------------------------\n"
    res_var = 0

    #try to execute python
    toExecute = ""+PYTHON_PATH+" \""+CONVERTER_PATH+"\" -i \""+v+"\" -o \""+v_out+".cs\" -xmldir \""+XML_DIR+"\""
    try:
      print toExecute+"\n"
      little_batch += toExecute+"\n"
      res_var = os.system(toExecute)
      pass
    except:
      res_var = 1
      pass
    print "Python Compiled = "+str(res_var)+"\n"
    #if python compiled
    if res_var == 0:
      print "Compiling\n"
      res_var = 0
      #try and compile the genereated python with the csharp compiler
      print os.getcwd()
      toExecute = ""+CSHARP_PATH+" "+v_out+".cs "+TESTDRIVER_PATH+" /reference:"+REFERENCES_DIR+"\\Kitware.VTK.dll;"+REFERENCES_DIR+"\\Kitware.mummy.runtime.dll"
      print toExecute+"\n"
      little_batch += toExecute+"\n"
      res_var = os.system(toExecute)
      print "CS Compiled = "+str(res_var)+"\n"

      #if the csharp compiled move this file to the compile directory
      if res_var == 0:
        #provide a batch file to recompile this test for debuging purposes
        output_file = file(""+DATE+"/Compiling/"+v_out+".bat","w")
        output_file.write(little_batch+"pause\n")
        output_file.close()

        toExecute = ""+CMAKE_PATH+" -E copy \""+v_out+".cs\" \""+DATE+"/Compiling\""
        print toExecute+"\n"
        little_batch += toExecute+"\n"
        os.system(toExecute)
        toExecute = ""+CMAKE_PATH+" -E remove \""+v_out+".cs\""
        print toExecute+"\n"
        little_batch += toExecute+"\n"
        os.system(toExecute)
        compilingTestsNum += 1
        compilingTests+= v+"\n"
        pass
      #if the csharp didn't compile move this file to the notcompiling directory
      else:
        #provide a batch file to recompile this test for debuging purposes
        output_file = file(""+DATE+"/NotCompiling/"+v_out+".bat","w")
        output_file.write(little_batch+"pause\n")
        output_file.close()

        toExecute = ""+CMAKE_PATH+" -E copy \""+v_out+".cs\" \""+DATE+"/NotCompiling\""
        little_batch += toExecute+"\n"
        print toExecute+"\n"
        os.system(toExecute)
        toExecute = ""+CMAKE_PATH+" -E remove \""+v_out+".cs\""
        little_batch += toExecute+"\n"
        print toExecute+"\n"
        os.system(toExecute)
        nonCompilingTestsNum += 1
        nonCompilingTests+= v+"\n"
        pass
      pass
    else:
      nonTranslatableTests += v+"\n"
      nonTranslatableTestsNum += 1
      pass
    print "------------------------\n"
    big_batch+= little_batch+"\n\n\n"
    little_batch = ""
    pass

  #print out statistics

  statistics = """

  There were """+str(totalTestsNum)+""" tests
  """+str(compilingTestsNum)+" - "+str(((compilingTestsNum*100)/totalTestsNum))+"""% Compile
  """+str(nonCompilingTestsNum)+" - "+str(((nonCompilingTestsNum*100)/totalTestsNum))+"""% Do Not Compile
  """+str(nonTranslatableTestsNum)+" - "+str(((nonTranslatableTestsNum*100)/totalTestsNum))+"""% Are Not Translatable
  """

  print statistics
  output_file = file(""+DATE+"/Statistics.txt","w")
  output_file.write(statistics)
  output_file.close()

  output_file = file(""+DATE+"/CompilingTests.txt","w")
  output_file.write(compilingTests)
  output_file.close()

  output_file = file(""+DATE+"/NonCompilingTests.txt","w")
  output_file.write(nonCompilingTests)
  output_file.close()

  output_file = file(""+DATE+"/NonTranslatableTests.txt","w")
  output_file.write(nonTranslatableTests)
  output_file.close()

  output_file = file(""+DATE+"/batch.bat","w")
  output_file.write(big_batch)
  output_file.close()
  pass



