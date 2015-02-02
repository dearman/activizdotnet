import sys

for i in range(0, len(sys.argv)):
    if sys.argv[i] == "-vtkdir" and i < len(sys.argv)-1:
            input_file = sys.argv[i+1]           
if  not input_file:
    print "Usage: %s [-vtkdir <path to the vtk source tree>] [-k ...] ..." % sys.argv[0]
    print "Got Args: %s" % `sys.argv`
    sys.exit(1)
input_file = input_file+"/Common/Core/vtkCommand.h"
fp = file(input_file, "r")
fp = fp.readlines()

output = "vtkEventIDs = [\n"
Start = False

for f in fp:
  # Old style:
  if f.strip() == "};":
    Start = False
  # New style:
  if f.strip() == "#define vtkEventDeclarationMacro(_enum_name)\\":
    Start = False
    output += "\"UserEvent\",\n"

  if Start == True:
    if f.find("*/")<0:
      if f.count("//")>0:
        f=f[:f.find("//")]
      if f.count("=")>0:
        f=f[:f.find("=")]
      # Old style:
      f = f.replace(",","")
      # New style:
      f = f.replace("_vtk_add_event(","")
      f = f.replace(")","")
      f = f.replace("\\","")
      event_name = f.replace(" ","").replace("\n","")
      if event_name != "":
        output += "\"" + event_name + "\"" + ",\n"

  # Old style:
  if f.strip().replace(" ","").replace("\n","") == "enumEventIds{":
    Start = True
  # New style:
  if f.strip().replace(" ","").replace("\n","") == "#definevtkAllEventsMacro()\\":
    Start = True
    output += "\"NoEvent\",\n"


output = output[:-2] + """
]

def get_vtk_event_ids():
  return vtkEventIDs
"""

try:
   ofp = file("vtkCommandList.py.in", "w")
   ofp.write(output)
   ofp.close()
except:
   print "Failed to write output file %s" % output
   sys.exit(1)
