#include "vtkObject.h"
#include "vtkObjectFactory.h"
#include "vtkSmartPointer.h"

//----------------------------------------------------------------------------
// We are intentionally referencing uninitialized data in this test, so
// please do not warn us about it...
//
#if defined(_MSC_VER)
#pragma warning(disable:4700)
#endif

#if !defined(_MSC_VER)
#define __int64 long long
#endif

//----------------------------------------------------------------------------
// This class is non-vtkObject derived so we can allocate one from the stack
// as well as the heap...
//
class UninitializedHelper
{
public:
  bool Bool;
  char Char;
  signed char SignedChar;
  unsigned char UnsignedChar;
  short Short;
  unsigned short UnsignedShort;
  int Int;
  unsigned int UnsignedInt;
  long Long;
  unsigned long UnsignedLong;
  __int64 Int64;
  unsigned __int64 UnsignedInt64;
  float Float;
  double Double;
  bool* BoolPointer;
  unsigned __int64* UnsignedInt64Pointer;
  double* DoublePointer;
  void* VoidPointer;
  vtkObject* ObjectPointer;
};

//----------------------------------------------------------------------------
class vtkUninitializedObject : public vtkObject
{
public:
  static vtkUninitializedObject* New();
  vtkTypeRevisionMacro(vtkUninitializedObject, vtkObject);

  virtual void PrintSelf(ostream& os, vtkIndent indent);

protected:
  vtkUninitializedObject();
  ~vtkUninitializedObject();

private:
  UninitializedHelper HeapHelper;
};

//----------------------------------------------------------------------------
vtkCxxRevisionMacro(vtkUninitializedObject, "$Revision: 436 $");
vtkStandardNewMacro(vtkUninitializedObject);

//----------------------------------------------------------------------------
vtkUninitializedObject::vtkUninitializedObject()
{
  // Do not initialize any data members here in the constructor.
  // This test exists to demonstrate the effect of having uninitialized data
  // in an object. Observe the output of the PrintSelf method, grasshopper.
  // And then remember: you must always initialize your data members to
  // prevent the unnecessary waste of another person's valuable time.
}

//----------------------------------------------------------------------------
vtkUninitializedObject::~vtkUninitializedObject()
{
}

//----------------------------------------------------------------------------
static void PrintHelperObject(ostream& os, vtkIndent indent,
  const UninitializedHelper& helper)
{
  os << indent << "Bool: " << helper.Bool << endl;

  // Cast the chars to "short equivalents" because streaming a char with
  // value 0 truncates the output when the ostream being passed in is an
  // ostringstream and the caller eventually uses ".c_str()" to write it
  // out... (And the value 0 is a likely value for an uninitialized chunk
  // of memory...)
  //
  os << indent << "(unsigned short) Char: " << (unsigned short) helper.Char << endl;
  os << indent << "(short) SignedChar: " << (short) helper.SignedChar << endl;
  os << indent << "(unsigned short) UnsignedChar: " << (unsigned short) helper.UnsignedChar << endl;

  os << indent << "Short: " << helper.Short << endl;
  os << indent << "UnsignedShort: " << helper.UnsignedShort << endl;
  os << indent << "Int: " << helper.Int << endl;
  os << indent << "UnsignedInt: " << helper.UnsignedInt << endl;
  os << indent << "Long: " << helper.Long << endl;
  os << indent << "UnsignedLong: " << helper.UnsignedLong << endl;
  os << indent << "Int64: " << helper.Int64 << endl;
  os << indent << "UnsignedInt64: " << helper.UnsignedInt64 << endl;
  os << indent << "Float: " << helper.Float << endl;
  os << indent << "Double: " << helper.Double << endl;
  os << indent << "BoolPointer: " << helper.BoolPointer << endl;
  os << indent << "UnsignedInt64Pointer: " << helper.UnsignedInt64Pointer << endl;
  os << indent << "DoublePointer: " << helper.DoublePointer << endl;
  os << indent << "VoidPointer: " << helper.VoidPointer << endl;
  os << indent << "ObjectPointer: " << helper.ObjectPointer << endl;
}

//----------------------------------------------------------------------------
void vtkUninitializedObject::PrintSelf(ostream& os, vtkIndent indent)
{
  this->Superclass::PrintSelf(os, indent);


  UninitializedHelper StackHelper;


  os << endl;
  os << indent <<
    "CTEST_FULL_OUTPUT (Avoid ctest truncation of output)" << endl;

  os << endl;
  os << indent << "Uninitialized heap data (default print format)" << endl;
  os << indent << "==============================================" << endl;
  PrintHelperObject(os, indent, this->HeapHelper);

  os << endl;
  os << indent << "Uninitialized stack data (default print format)" << endl;
  os << indent << "===============================================" << endl;
  PrintHelperObject(os, indent, StackHelper);


  os.setf(os.hex, os.basefield);


  os << endl;
  os << indent << "Uninitialized heap data (hex print format)" << endl;
  os << indent << "==========================================" << endl;
  PrintHelperObject(os, indent, this->HeapHelper);

  os << endl;
  os << indent << "Uninitialized stack data (hex print format)" << endl;
  os << indent << "===========================================" << endl;
  PrintHelperObject(os, indent, StackHelper);
}

//----------------------------------------------------------------------------
int IntentionallyUninitializedData(int argc, char *argv[])
{
  vtkSmartPointer<vtkUninitializedObject> obj =
    vtkSmartPointer<vtkUninitializedObject>::New();
  obj->Print(cerr);

  return 0;
}
