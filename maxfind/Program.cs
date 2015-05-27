using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maxfind
{
    class Program
    {
        static void CheckFile(ref StreamWriter outfile, string searchstring, string filename)
        {
            try
            {
                StreamReader rdr = new StreamReader(filename, true);

                int linenum = 0;
                string linestr = rdr.ReadLine();
                while (linestr != null)
                {
                    linenum++;

                    if (linestr.Contains(searchstring))
                    {
                        outfile.WriteLine(string.Format("{0} line{1}: {2}", filename, linenum, linestr));
                    }

                    linestr = rdr.ReadLine();
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                string errorstr = "maxfind: CheckFile caught exception!";
                Console.WriteLine(errorstr);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                System.Environment.FailFast("maxfind CheckFile FailFast");
            }
            finally
            {
            }
        }

        static void ScanDirectory(ref StreamWriter outfile, string searchstring, string searchpath)
        {
            try
            {
                DirectoryInfo mydirinfo = new DirectoryInfo(searchpath);
                FileInfo[] folderfiles = mydirinfo.GetFiles();
                for (int ii = 0; ii < folderfiles.Length; ii++)
                {
                    string fullpath = folderfiles[ii].FullName;
                    CheckFile(ref outfile, searchstring, fullpath);

                //    string basename = folderfiles[ii].Name;
                //    source_basename.Add(basename);

                //    string dirname = folderfiles[ii].DirectoryName;
                //    source_dirname.Add(dirname);
                }

                DirectoryInfo[] folderdirs = mydirinfo.GetDirectories();
                for (int ii = 0; ii < folderdirs.Length; ii++)
                {
                    ScanDirectory(ref outfile, searchstring, folderdirs[ii].FullName);
                }
            }
            catch (Exception ex)
            {
                string errorstr = "maxfind: ScanDirectory caught exception!";
                Console.WriteLine(errorstr);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                System.Environment.FailFast("maxfind ScanDirectory FailFast");
            }
        }//ScanDirectory

        static void Main(string[] args)
        {
            for (int ii = 0; ii < args.Length; ii++)
                Console.WriteLine(string.Format("args[{0}] = {1}", ii, args[ii]));

            StreamWriter outfile = new StreamWriter("maxfindout.txt");
            ScanDirectory(ref outfile, args[0], args[1]);
            outfile.Close();
        }//Main
    }//class Program
}//namespace
