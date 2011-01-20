//----------------------------------------------------------------------------
extern "C" MUMMY_DLL_EXPORT
const char* vtkStringArray_GetValue(vtkStringArray* pThis, long long id)
{
   vtkStdString& rvmi = pThis->GetValue(static_cast<vtkIdType>(id));

   const char* rvm = rvmi.c_str();
   size_t n = 0;

   if (rvm)
      {
      n = strlen(rvm);
      }

   char* rv = (char*) MUMMY_STRING_ALLOC(n+1);
   if (rv)
      {
      strncpy(rv, rvm, n);
      rv[n] = 0;
      }

   return rv;
}
