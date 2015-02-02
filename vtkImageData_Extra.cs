///<summary>
///Creates a vtkImageData object from a System.Drawing.Image
///</summary>
///<param name="img">The System.Drawing.Image to convert</param>
public static vtkImageData FromImage(System.Drawing.Image img)
{
  return vtkImageData.FromImage(img,4);
}
///<summary>
///Creates a vtkImageData object from a System.Drawing.Image
///</summary>
///<param name="img">The System.Drawing.Image to convert</param>
///<param name="numberOfScalarComponents">3 for RGB and 4 for RGBA</param>
public static vtkImageData FromImage(System.Drawing.Image img,int numberOfScalarComponents)
{
  System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
  vtkImageData idata = vtkImageData.New();

// TODO: revert this #if False block later after updating code to work
// with current vtkImageData method names
#if False
  idata.SetScalarTypeToUnsignedChar();

  idata.SetNumberOfScalarComponents(numberOfScalarComponents);
  idata.SetDimensions(img.Width, img.Height, 1);
  idata.AllocateScalars();
  byte[] data = new byte[img.Width * img.Height * idata.GetNumberOfScalarComponents()];
  int index = 0;
  for (int i = img.Height-1; i >=0; i--)
    {
    for (int j = 0; j < img.Width; j++)
      { 
      data[index++] = bmp.GetPixel(j, i).R;
      data[index++] = bmp.GetPixel(j, i).G;
      data[index++] = bmp.GetPixel(j, i).B; 
      if(numberOfScalarComponents>3)
        {
        data[index++] = bmp.GetPixel(j, i).A;
        }
      }
    }
  System.Runtime.InteropServices.Marshal.Copy(data, 0,(IntPtr) idata.GetScalarPointer(), data.Length);
#endif
  return idata;
}
///<summary>
///Returns a System.Drawing.Bitmap created from a plane
///of the vtkImageData
///</summary>
public System.Drawing.Bitmap ToBitmap()
{
  vtkImageData idata = this;
  int width = (int)(idata.GetBounds()[1] - idata.GetBounds()[0])+1;
  int height = (int)(idata.GetBounds()[3] - idata.GetBounds()[2])+1;
  System.Drawing.Bitmap img = new System.Drawing.Bitmap((int)width,(int)height);
  byte[] dest = new byte[(int)width * (int)height * idata.GetNumberOfScalarComponents()];

  System.Runtime.InteropServices.Marshal.Copy(
      ((IntPtr)idata.GetScalarPointer()),
      dest, 
      0,
      (int)width * (int)height * idata.GetNumberOfScalarComponents());

  int index = 0;
  for (int i = height - 1; i >= 0; i--)
    {
    for (int j = 0; j < width; j++)
      {
      if (idata.GetNumberOfScalarComponents() == 3)
        {
        img.SetPixel(j, i, System.Drawing.Color.FromArgb(dest[index++], dest[index++], dest[index++]));
        }
      else if (idata.GetNumberOfScalarComponents() == 4)
        {
        img.SetPixel(j, i, System.Drawing.Color.FromArgb(dest[index++], dest[index++], dest[index++], dest[index++]));
        }
      else
        {
        throw new Exception("Invalid Number of Scalar Components");
        }
      }
    }
  return img;
}
