import xml.dom.minidom
import TclToCsHashMap

already_opened_files = TclToCsHashMap.TclToCsHashMap()
xml_dir = "C:/Activiz Build/xml"

def print_tree(document, spaces):

  for i in range(len(document.childNodes)):
    if str(document.childNodes[i])[:14]!="<DOM Text node":
      print spaces+str(document.childNodes[i].getAttribute('id') )
    print_tree(document.childNodes[i],spaces+"----")
  pass

def get_element(element, attribute_type, attribute_value):
  if element.nodeType == 1:
    if element.getAttribute(attribute_type)==attribute_value :
      return element   
  for i in range(len(element.childNodes)):
    toReturn = get_element(element.childNodes[i],attribute_type,attribute_value)
    if(toReturn!=None):
      return toReturn
    pass
  return None

def get_all_elements(element, attribute_type, attribute_value, list = []):
  if element.nodeType == 1:
    if(element.getAttribute(attribute_type)==attribute_value):
      list.append(element)
  for i in range(len(element.childNodes)):
    list = (get_all_elements(element.childNodes[i],attribute_type,attribute_value,list))
  return list

def get_params_type(class_name,method_name):
  try:
    doc1 = None
    if class_name in already_opened_files.keys:
      doc1 = already_opened_files.GetItem(class_name)
    else:
      try:
        doc1 = xml.dom.minidom.parse(xml_dir+"/"+class_name+".xml")
        already_opened_files.AddItem(class_name,doc1)
      except:
        doc1 = None
    if doc1 == None:
      return None
    doc = doc1
    list = []
    elements = get_all_elements(doc, 'name', method_name,list)
    list = []
    for el in elements:
      demangled = el.getAttribute('demangled')
      demangled = demangled[demangled.find("(")+1:demangled.find(")")]
      tempList= demangled.split(",")
      for i in range(len(tempList)):
        tempList[i] = tempList[i].replace("*","")
        tempList[i] = tempList[i].replace(" ","")
        tempList[i] = tempList[i].replace("unsigned","u")
        tempList[i] = tempList[i].replace("const","")
        tempList[i] = tempList[i].replace("long","int")
        tempList[i] = tempList[i].replace("intint","long")
        if tempList[i] == "u":
          tempList[i] = "uint"
        elif tempList[i] == "uchar":
          tempList[i] = "byte"
        elif tempList[i] == "char":
          tempList[i] = "string"
      list.append(tempList)
    return list
  except:
    import sys
    print sys.exc_info()[0]
    return None

def superclass(class_name):
  try:
    doc1 = None
    if class_name in already_opened_files.keys:
      doc1 = already_opened_files.GetItem(class_name)
    else:
      try:
        doc1 = xml.dom.minidom.parse(xml_dir+"/"+class_name+".xml")
        already_opened_files.AddItem(class_name,doc1)
      except:
        doc1 = None
    if doc1 == None:
      return False
    doc = doc1
    doc = get_element(doc, 'name', class_name)
    returns = doc.getAttribute('bases')
    doc = doc1
    doc = get_element(doc, 'id', returns.replace(" ",""))
    name = doc.getAttribute('name')
    return name
  except:
    import sys
    print sys.exc_info()[0]
    return None

def is_static_method(class_name,method_name):
   try:
    doc1 = None
    if class_name in already_opened_files.keys:
      doc1 = already_opened_files.GetItem(class_name)
    else:
      try:
        doc1 = xml.dom.minidom.parse(xml_dir+"/"+class_name+".xml")
        already_opened_files.AddItem(class_name,doc1)
      except:
        doc1 = None
    if doc1 == None:
      return False
    doc = doc1
    doc = get_element(doc, 'name', method_name)
    return (doc.getAttribute('static')=="1")
   except:
    return False

def look_up_method_return_type(class_name,method_name):
    #special cases:
   try:
    if method_name=="GetNumberOfCells":
      return "int"
    if method_name == "GetPointData":
      return "vtkPointData"
    doc1 = None
    if class_name in already_opened_files.keys:
      doc1 = already_opened_files.GetItem(class_name)
    else:
      doc1 = xml.dom.minidom.parse(xml_dir+"/"+class_name+".xml")
      already_opened_files.AddItem(class_name,doc1)
    doc = doc1
    doc = get_element(doc, 'name', method_name)
    returns = doc.getAttribute('returns')
    doc = doc1
    doc = get_element(doc, 'id', returns)
    name = doc.getAttribute('name')
    pointer = ""
    #loops through typedefs
    while doc.localName == "Typedef":
      type = doc.getAttribute('type')
      doc = doc1
      doc = get_element(doc, 'id', type)
      name = doc.getAttribute('name')
      pass
    while(name==""):
      type = doc.getAttribute('type')
      doc = doc1
      doc = get_element(doc, 'id', type)
      name = doc.getAttribute('name')
      #loops through typedefs
      while doc.localName == "Typedef":
        type = doc.getAttribute('type')
        doc = doc1
        doc = get_element(doc, 'id', type)
        name = doc.getAttribute('name')
        pass
      else:
        pointer += "*"
        pass
      pass
    name = name+pointer
    name = name.replace("long long int","long")
    return name
   except:
    pass



