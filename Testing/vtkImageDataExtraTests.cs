using System;
using Kitware.VTK;
using System.Drawing;
// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkImageDataExtraTestsClass
{

        static int numScalarComponents = 3;
    // Static void method with same signature as "Main" is always
    // file base name:
    //
    /// <summary>
    /// VTK test Main method
    /// </summary>
    public static void vtkImageDataExtraTests(string[] args)
    {
        string vtkDataDir = "";

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-D")
            {
                vtkDataDir = args[i + 1];
            }
        }


        //Read in bitmap and image data
        string bmpFile = vtkDataDir + "/Data/masonry.bmp";
        vtkImageReader2 rdr = vtkImageReader2Factory.CreateImageReader2(bmpFile);
        rdr.SetFileName(bmpFile);
        rdr.Update();
        vtkImageData idata = rdr.GetOutput();
        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bmpFile);

        //get bytes before and after double conversion
        byte[] bmpArr1 = getArrayFromBitmap(bmp);
        vtkImageData bmp1 = vtkImageData.FromImage(bmp,3);
        Bitmap bmp2 = bmp1.ToBitmap();
        return;

        byte[] bmpArr2 = getArrayFromBitmap(bmp2);
       
        //get bytes before and after double conversion
        byte[] idataArr1 = getArrayFromImageData(idata);
        Bitmap idata1 = idata.ToBitmap();
        vtkImageData idata2 = vtkImageData.FromImage(idata1,3);
        byte[] idataArr2 = getArrayFromImageData(idata2);

       
        for (int i = 0; i < bmpArr1.Length; i++)
        {
            if (bmpArr1[i] != bmpArr2[i])
            {
                throw new Exception("ERROR: Images Not Identical!");
            }
        }
        for (int i = 0; i < idataArr2.Length; i++)
        {
            if (idataArr1[i] != idataArr2[i])
            {
                throw new Exception("ERROR: Images Not Identical!");
            }
        }
        rdr.Dispose();
        Console.Out.WriteLine("Passed");
    }
    /// <summary>
    /// Creates byte array from vtkImageData
    /// </summary>
    /// <param name="bmp"></param>
    /// <returns></returns>
    static public byte[] getArrayFromBitmap(Bitmap bmp)
    {
        byte[] arr = new byte[bmp.Width * bmp.Height * numScalarComponents];
        int index = 0;
        for (int i = bmp.Height - 1; i >= 0; i--)
        {
            for (int j = 0; j < bmp.Width; j++)
            {
                arr[index++] = bmp.GetPixel(j, i).R;
                arr[index++] = bmp.GetPixel(j, i).G;
                arr[index++] = bmp.GetPixel(j, i).B;
                if (numScalarComponents > 3)
                {
                    arr[index++] = bmp.GetPixel(j, i).A;
                }
            }
        }
        return arr;
    }
    /// <summary>
    /// Creates byte array from vtkImageData
    /// </summary>
    /// <param name="idata"></param>
    /// <returns></returns>
    static public byte[] getArrayFromImageData(vtkImageData idata)
    {
        int width = (int)(idata.GetBounds()[1] - idata.GetBounds()[0]) + 1;
        int height = (int)(idata.GetBounds()[3] - idata.GetBounds()[2]) + 1;
        byte[] arr = new byte[width * height * idata.GetNumberOfScalarComponents()];
        System.Runtime.InteropServices.Marshal.Copy(
            ((IntPtr)idata.GetScalarPointer()),
            arr,
            0,
            width * height * idata.GetNumberOfScalarComponents());
        return arr;
    }
}
