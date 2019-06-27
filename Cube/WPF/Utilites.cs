using Merlion.ECR.Update.Core.Manifest;
using System.Collections.Generic;
using coreConst = Merlion.ECR.Update.Core.Constants;

namespace RTCManifestGenerator
{
    static class Utilites
    {
        public static ManifestFileItem GenerateFileItem(ManifestFileGridItem item)
        {
            var rtc = new ManifestFileItem();
            rtc.FileName = item.FileName;
            rtc.CRC = GetFileHash(item.FullPatName);
            if (item.Folders == null)// || string.IsNullOrWhiteSpace(item.Folders))
                rtc.AddFolder(coreConst.Manifest.RootDirectoryFlag);
            else
            {
                foreach (var folder in ItemFoldersManager.GetFolders(item.Folders))
                    rtc.AddFolder(folder.Trim());
            }
            //if (item.AddInsFolder) rtc.AddFolder("Add-ins");
            //if (item.ArcusFolder) rtc.AddFolder("Arcus2");
            rtc.Type = item.Type;
            rtc.Version = item.Version;

            var manifestItem = item as IManifestFileItem;
            rtc.GAC = (RegInfo)manifestItem.GAC;
            rtc.REGSRV32 = (RegInfo)manifestItem.REGSRV32;
            rtc.REGASM = (RegInfo)manifestItem.REGASM;
            rtc.Delete = item.Delete;
            rtc.AddinName = item.AddinName;
            rtc.PublicKeyToken = item.PublicKeyToken;

            return rtc;
        }

        public static string GetFileHash(string pathName)
        {
            return Merlion.ECR.Update.Core_2_0.Utilites.GetFileHash(pathName);
        }

        public static IEnumerable<string> GetAllManifestFileActions
        {
            /*
            получение всех констант Действия 
            (через рефлексию, чтобы потом не забыть при добавлении новых)
            для ComboxBox Actions

                static "DependencyProperty"
            */
            get
            {
                var type = typeof(coreConst.Manifest.Actions);

                var actionsList = new List<string>();
                foreach (var field in type.GetFields())
                    actionsList.Add(field.GetValue(null) as string);

                return actionsList;
            }
        }
    }
}
