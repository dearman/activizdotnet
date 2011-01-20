   [DllImport(vtkCommonEL_dll, EntryPoint = "vtkStringArray_GetValue")]
   internal static extern string vtkStringArray_GetValue(HandleRef pThis, long id);

   /// <summary>
   /// Get the data at a particular index.
   /// </summary>
   public string GetValue(long id)
   {
      return vtkStringArray_GetValue(this.GetCppThis(), id);
   }
