   /// <summary>
   /// Managed/.NET signature for handlers of the SetExecuteMethod callback.
   /// </summary>
   public delegate void ExecuteMethodHandler(IntPtr arg);

   [DllImport(vtkFiltersProgrammableEL_dll, EntryPoint = "vtkProgrammableAttributeDataFilter_SetExecuteMethod")]
   internal static extern void vtkProgrammableAttributeDataFilter_SetExecuteMethod(HandleRef pThis, ExecuteMethodHandler handler, IntPtr arg);

   /// <summary>
   /// Specify the function to use to operate on the point attribute data. Note
   /// that the function takes a single (void *) argument.
   /// </summary>
   public void SetExecuteMethod(ExecuteMethodHandler handler, IntPtr arg)
   {
      vtkProgrammableAttributeDataFilter_SetExecuteMethod(this.GetCppThis(), handler, arg);
   }

   /// <summary>
   /// Specify the function to use to operate on the point attribute data. Note
   /// that the function takes a single (void *) argument.
   /// </summary>
   public void SetExecuteMethod(ExecuteMethodHandler handler)
   {
     vtkProgrammableAttributeDataFilter_SetExecuteMethod(this.GetCppThis(), handler, IntPtr.Zero);
   }
