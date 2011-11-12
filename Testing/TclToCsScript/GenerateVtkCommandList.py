import sys

for i in range(0, len(sys.argv)):
    if sys.argv[i] == "-vtkdir" and i < len(sys.argv)-1:
            input_file = sys.argv[i+1]           
if  not input_file:
    print "Usage: %s [-vtkdir <path to the vtk source tree>] [-k ...] ..." % sys.argv[0]
    print "Got Args: %s" % `sys.argv`
    sys.exit(1)
input_file = input_file+"/Common/vtkCommand.h"
fp = file(input_file, "r")
fp = fp.readlines()
output = "vtkEventIDs = ["
Start = False
for f in fp:
  if f.strip() == "};":
    Start = False
  if Start == True:
    if f.count("//")>0:
      f=f[:f.find("//")]
    if f.count("=")>0:
      f=f[:f.find("=")]
    f = f.replace(",","")
    output+="\""+f.replace(" ","").replace("\n","")+"\",\n"
  if f.strip().replace(" ","").replace("\n","") == "enumEventIds{":
    Start = True
output = output[:-2]+"""]    

def get_vtk_event_ids():
  return vtkEventIDs"""

try:
   ofp = file("vtkCommandList.py.in", "w")
   ofp.write(output)
   ofp.close()
except:
   print "Failed to write output file %s" % output
   sys.exit(1)

