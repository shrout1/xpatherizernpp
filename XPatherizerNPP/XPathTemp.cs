using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Kbg.NppPluginNET
{
    class XPathTemp
    {
        private ArrayList tempFiles;
        private ArrayList tempXPaths;
        private string CurrentFile;
        private string TempFilename;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tempFilename">Full path and Filename to the data file.</param>
        public XPathTemp(string tempFilename)
        {
            tempFiles = new ArrayList();
            tempXPaths = new ArrayList();
            TempFilename = tempFilename;
            try
            {
                Load();
            }
            catch { }
        }

        /// <summary>
        /// Save to the current file.
        /// </summary>
        /// <param name="XPath">The XPath to save to the file</param>
        public void Save(string XPath)
        {
            tempXPaths[tempFiles.IndexOf(CurrentFile)] = XPath;
            Save();
        }

        /// <summary>
        /// Load the XPath and set the current file.
        /// </summary>
        /// <param name="file">Filename to load data for.  New line added if not found.</param>
        /// <returns>string: the XPath for the file.  Empty string if not found.</returns>
        public string Load(string file)
        {
            CurrentFile = file;
            if (tempFiles.IndexOf(file) > -1)
            {
                return tempXPaths[tempFiles.IndexOf(file)].ToString();
            }
            else
            {
                tempFiles.Add(file);
                tempXPaths.Add("");
                return "";
            }
        }

        /// <summary>
        /// Load the data from the data file.
        /// </summary>
        private void Load()
        {
            try
            {
                FileStream fs = File.OpenRead(TempFilename);
                BinaryReader br = new BinaryReader(fs);
                while (fs.Position < fs.Length)
                {
                    tempFiles.Add(br.ReadString());
                    tempXPaths.Add(br.ReadString());
                }
                br.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("XPath File Load Failed with message:\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Save the data to the data file.
        /// </summary>
        public void Save()
        {
            try
            {
                FileStream fs = File.Open(TempFilename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                for (int i = 0; i < tempFiles.Count; i++)
                {
                    bw.Write(tempFiles[i].ToString());
                    bw.Write(tempXPaths[i].ToString());
                }
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("XPath File Save Failed with message:\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Remove a single row from the data file.
        /// </summary>
        /// <param name="file">The file name to remove data for.</param>
        public void Delete(string file)
        {
            int i = tempFiles.IndexOf(file);
            if (i > -1)
            {
                tempFiles.RemoveAt(i);
                tempXPaths.RemoveAt(i);
            }
        }

        /// <summary>
        /// Verify that all the files exist.  Any that do not will be removed from the data file.
        /// </summary>
        /// <param name="files">List of files that do exist.</param>
        public void Verify(ArrayList files)
        {
            ArrayList temp = new ArrayList();
            for (int i = 0; i < tempFiles.Count; i++)
                temp.Add(0);
            foreach (string s in files)
            {
                if (tempFiles.IndexOf(s) > -1)
                    temp[tempFiles.IndexOf(s)] = 1;
            }
            while (temp.IndexOf(0) > -1)
            {
                int j = temp.IndexOf(0);
                tempFiles.RemoveAt(j);
                tempXPaths.RemoveAt(j);
                temp.RemoveAt(j);
            }
            Save();
        }
    }
}
