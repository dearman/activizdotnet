import sys
import re
import string

class TclToCsHashMap:
  keys = []
  values = []
  def __init__(self):
    self.keys = []
    self.values = []

  def AddItem(self, key, value):
    for i in range(len(self.keys)):
      if key == self.keys[i]:
       # print "Warning: Key already exists, value overwritten\n"
        self.values[i] = value
        return -1
      pass
    self.keys.append(key)
    self.values.append(value)

  def RemoveItem(self, key):
    tempItem = self.GetItem(key)
    self.keys.pop(self.getItemIndex(key))
    self.values.pop(self.getItemIndex(key))
    pass

  def GetItemIndex(self,key):
    for i in range(len(self.keys)):
      if key == self.keys[i]:
        return i

  def GetItem(self,key):
    try:
      return self.values[self.GetItemIndex(key)]
    except:
      return None

  def CombineWith(self,p):
    for i in range(len(p.keys)):
      self.AddItem(p.keys[i],p.values[i])
