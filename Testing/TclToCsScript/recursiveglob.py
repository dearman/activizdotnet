##
## Recurive glob example
## 9/29/2006
## hosted by: Berlin Brown
##
## jython -c "import blb.DirAll as b;b.main()" -v
##

import os
import glob
import sys
import fnmatch

#
# Globals 
verboseflag = 0
pathaccess_ctr = 0

def glob_get_dirs(path):
    ''' Return a list of directorie at this particular path '''
    global verboseflag
    global pathaccess_ctr

    d = []
    if verboseflag:
        print "  scanning=*", path
    else:
        pathaccess_ctr = pathaccess_ctr + 1
        #print ".",
        if pathaccess_ctr == 40:
            print

    try:
        for i in os.listdir(path):          
            if os.path.isdir(path+i):
                d.append(os.path.basename(i))

    except NameError, ne:
        print "NameError thrown=", ne
    except:
        print sys.exc_info()[0]
        print "ERR in get_dirs()"

    return d

def rec_glob(path, mask):
    ''' Recursive glob on the following path '''
    l = []
    if path[-1] != '\\':
        path = path + '\\'

    for i in glob_get_dirs(path):
        res = rec_glob(path + i, mask)
        l = l + res

    try:
        for i in os.listdir(path):
            ii = i
            i = path + i
            if os.path.isfile(i):
                if fnmatch.fnmatch(ii, mask):
                    l.append(i)
                    if verboseflag:
                        print "file=", i

    except NameError, ne:
        print "NameError thrown=", ne
    except:
        print sys.exc_info()[0]
        print "ERR in rec_glob()"

    return l    

# End of the File

