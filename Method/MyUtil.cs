using Iteedee.ApkReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernateGenLib.Domain.AppUpload;

namespace WindowsFormsUploadApp.Method
{
    public class MyUtil
    {
        public static ApkInfo ReadApkInfo(String apkPath)
        {
            //string apkPath = "C:\\Users\\Administrator\\Desktop\\MyModibleAssistant.apk";
            using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(apkPath)))
            {
                using (var filestream = new FileStream(apkPath, FileMode.Open, FileAccess.Read))
                {
                    ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry item;

                    byte[] manifestData = null;
                    byte[] resourcesData = null;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == "androidmanifest.xml")
                        {
                            manifestData = new byte[1024 * 1024 * 100];
                            using (Stream strm = zipfile.GetInputStream(item))
                            {
                                strm.Read(manifestData, 0, manifestData.Length);
                            }

                        }
                        if (item.Name.ToLower() == "resources.arsc")
                        {
                            using (Stream strm = zipfile.GetInputStream(item))
                            {
                                using (BinaryReader s = new BinaryReader(strm))
                                {
                                    resourcesData = s.ReadBytes((int)s.BaseStream.Length > 0 ? (int)s.BaseStream.Length : 99999999);
                                }
                            }
                        }
                    }
                    ApkReader apkReader = new ApkReader();
                    ApkInfo info = apkReader.extractInfo(manifestData, resourcesData);

                    return info;
                }
            }
        }


        public static void ExtractFileAndSave(string APKFilePath, string fileResourceLocation, string FilePathToSave, String fileName)
        {
            using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(APKFilePath)))
            {
                using (var filestream = new FileStream(APKFilePath, FileMode.Open, FileAccess.Read))
                {
                    ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == fileResourceLocation)
                        {
                            //string fileLocation = Path.Combine(FilePathToSave, string.Format("{0}-{1}", index, fileResourceLocation.Split(Convert.ToChar(@"/")).Last()));
                            string fileLocation = Path.Combine(FilePathToSave, fileName);
                            using (Stream strm = zipfile.GetInputStream(item))
                            using (FileStream output = File.Create(fileLocation))
                            {
                                try
                                {
                                    strm.CopyTo(output);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                    }
                }
            }
        }


        public static TbApp a(String board, String uploadFile, String uploadPath)
        {
            TbApp app = new TbApp();

            var info = ReadApkInfo(uploadFile);            
            app.Pkg = info.packageName;
            app.VerCode = info.versionCode;
            app.VerName = info.versionName;

            app.FileSize = (int)new FileInfo(uploadFile).Length;
            String iconPath = Path.Combine(uploadPath, "icon");
            if (!Directory.Exists(iconPath)) Directory.CreateDirectory(iconPath);
            String iconName = DateTime.Now.Ticks + ".png";            
            ExtractFileAndSave(uploadFile, info.iconFileName[info.iconFileName.Count - 1], iconPath, iconName);
            app.IconPath = Path.Combine(iconPath, iconName);            
            String newApkName = Path.Combine(uploadPath, board, Path.GetFileName(uploadFile));
            if (File.Exists(newApkName))
                for (int i = 1; i < 10000; i++)
                {
                    newApkName = Path.Combine(uploadPath, board, Path.GetFileNameWithoutExtension(uploadFile)+"("+i+")"+Path.GetExtension(uploadFile));
                    if (!File.Exists(newApkName)) break;
                }
            if (!Directory.Exists(Path.GetDirectoryName(newApkName))) Directory.CreateDirectory(Path.GetDirectoryName(newApkName));
            File.Copy(uploadFile, newApkName);
            app.SavePath = newApkName;

            return app;
        }

        public static void DelAppFiles(TbApp app)
        {
            File.Delete(app.IconPath);
            File.Delete(app.SavePath);
        }
    }
   
}
