//----------------------------------------------------------------------------
extern "C" MUMMY_DLL_EXPORT
void vtkProgrammableAttributeDataFilter_SetExecuteMethod(vtkProgrammableAttributeDataFilter* pThis, void (*f)(void *), void *arg)
{
  pThis->SetExecuteMethod(f, arg);
}
