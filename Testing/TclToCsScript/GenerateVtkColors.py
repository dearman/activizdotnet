import sys

for i in range(0, len(sys.argv)):
    if sys.argv[i] == "-vtkdir" and i < len(sys.argv)-1:
            input_file = sys.argv[i+1]           
if  not input_file:
    print "Usage: %s [-vtkdir <path to the vtk source tree>] [-k ...] ..." % sys.argv[0]
    print "Got Args: %s" % `sys.argv`
    sys.exit(1)
input_file = input_file+"/Testing/Core/vtkTestingColors.h"
fp = file(input_file, "r")
fp = fp.readlines()
output = """class vtkColors:
  colors = []
  def __init__(self):
     self.colors = ["""
for f in fp:
  if f.strip()[0:1] in ["#","/"]:
    pass
  else:
    output += "     "+f.replace("static float ","\"").replace("{","\"").replace("}","\"").replace("[3] = ","\",").replace(";",",").replace("vtk_","")
output= output[:-8]+"]\n     pass"

try:
   ofp = file("vtkColors.py.in", "w")
   ofp.write(output)
   ofp.close()
except:
   print "Failed to write output file %s" % output
   sys.exit(1)
