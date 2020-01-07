//-----------------------------------------------------------------------
//
// Storage file routines for the GrampsStoreXML
//
// <copyright file="StoreFile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using ICSharpCode.SharpZipLib.GZip;
    using ICSharpCode.SharpZipLib.Tar;

    [DataContract]
    public partial class StoreFile : CommonBindableBase, IStoreFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreFile" /> class.
        /// </summary>
        /// <param name="iocCommonProgress">
        /// The ioc common progress.
        /// </param>
        public StoreFile()
        {
        }

        /// <summary>
        /// Deletes all local copies of GRAMPS data.
        /// </summary>
        /// <returns>
        /// Flag if data cleared.
        /// </returns>
        public async Task<bool> DataStorageInitialiseAsync()
        {
            await DataStore.CN.MajorStatusAdd("Deleting existing datastorage").ConfigureAwait(false);
            {
                try
                {
                    // TODO Fix - Clean DataFolder
                    //if (FileInfoEx.Exists(StoreFileUtility.GetLocalFolderPath()))
                    //{
                    //    // delete folder and all subfolders
                    //    File.Delete(StoreFileUtility.GetLocalFolderPath());
                    //}

                    // Create requested main folder
                    // StoreFileUtility.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CommonConstants.StorageInternalFolder);

                    //// Creeate bitmap cache folder
                    //StoreFileUtility.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CommonConstants.StorageThumbNailFolder);
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("DataStorageInitialiseAsync", ex);
                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <param name="inputFile">
        /// Input GRAMPS export file.
        /// </param>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressGZIP(FileInfoEx inputFile)
        {
            await DataStore.CN.MajorStatusAdd("Decompressing GRAMPS GZIP file").ConfigureAwait(false);

            // Check arguments
            if (inputFile == null)
            {
                DataStore.CN.NotifyError("The input file is null");
                return false;
            }

            try
            {
                // Common.TestFileSystem vfs = new TestFileSystem();

                // VFS.SetCurrent(vfs);
                await ExtractGZip(inputFile).ConfigureAwait(false);

                await DataStore.CN.MajorStatusAdd("GRAMPS file decompressing complete").ConfigureAwait(false);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                DataStore.CN.NotifyError("Unauthorised Access exception when trying to acess file. " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <param name="inputFile">
        /// Input GRAMPS export file.
        /// </param>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressTAR()
        {
            await DataStore.CN.MajorStatusAdd("Decompressing GRAMPS TAR file").ConfigureAwait(false);

            // bool onlyCopyIfModified = true;

            // Check arguments
            if (DataStore.DS.CurrentInputFile == null)
            {
                DataStore.CN.NotifyError("The input file is invalid");
                return false;
            }

            Stream originalFileStream = DataStore.DS.CurrentInputFile.GetStream();

            // TarArchive archive = null;

            // Common.TestFileSystem vfs = new TestFileSystem();

            // VFS.SetCurrent(vfs);

            // open the gzip and extract the tar file
            await DataStore.CN.MajorStatusAdd("Decompressing individual TAR files").ConfigureAwait(false);
            await DataStore.CN.MajorStatusAdd("This will take a while...").ConfigureAwait(false);

            using (Stream stream = new GZipInputStream(originalFileStream))
            {
                using (TarInputStream tarIn = new TarInputStream(stream))
                {
                    // DO NOT AWAIT as causes thread blocking await
                    // ExtractTarIfNotModified(dataFolder, tarIn, false);
                    await ExtractTarArchive(tarIn).ConfigureAwait(false);
                }
            }

            // await DataStore.CN.MajorStatusAdd("Decompressing Gramps XML file");

            // Stream inStream = File.OpenRead(localDataFolderPath.Path + "\\" + CommonConstants.localGRAMPSxmlFileName);

            // tarArchive.ExtractContents(localDataFolderPath.Path + "\\" + "camerons");

            // ExtractGZipSample(dataFolder.Path + "\\data.gramps", dataFolder.Path);

            // Delete file and copy new StorageFile oldFile = await
            // StorageFile.GetFileFromPathAsync(dataFolder.Path + "\\" + "data");

            // await oldFile.RenameAsync(CommonConstants.StorageGRAMPSXMLFileName, NameCollisionOption.ReplaceExisting);

            //// hide the progress bar await DataStore.CN.MajorStatusAdd("GRAMPS file loading complete");
            return true;
        }

        /// <summary>
        /// get the StorageFile of the file.
        /// </summary>
        /// <param name="relativeFilePath">
        /// file path relative to the provider base folder.
        /// </param>
        /// <returns>
        /// StorageFile for the chosen file.
        /// </returns>
        public async static Task<FileInfoEx> GetStorageFileAsync(string relativeFilePath)
        {
            FileInfoEx resultFile = new FileInfoEx();

            // Check for relative path
            if (!StoreFileUtility.IsRelativeFilePathValid(relativeFilePath))
            {
                return resultFile;
            }

            // load the real file
            DirectoryInfo tt = DataStore.DS.CurrentDataFolder;
            if (tt != null)
            {
                try
                {
                    if (Directory.Exists(Path.Combine(tt.FullName, Path.GetDirectoryName(relativeFilePath))))
                    {
                        FileInfo[] t = tt.GetFiles(relativeFilePath);

                        resultFile.FInfo = t[0];
                    }
                    return resultFile;
                }
                catch (FileNotFoundException ex)
                {
                    await DataStore.CN.MajorStatusAdd(ex.Message + ex.FileName).ConfigureAwait(false);

                    // default to a standard file marker
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException(ex.Message + relativeFilePath, ex);
                    throw;
                }
            }

            return resultFile;
        }
    }
}