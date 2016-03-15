using System;
using System.IO;
using System.Collections.Generic;

namespace ParrotLibs
{
    public enum DriveType
    {
        HardDisk,
        USB,
        Backup,
    }
    public enum DriveMagic
    {
        DEVKIT = 0x00020000,
        XTAF = 0x58544146
    }

    /// <summary>
    /// Handles FATX-formatted drives/files
    /// </summary>
    public class Drive
    {
        private long length = 0;
        string name = "";
        private Stream thisStream;
        private Streams.Reader thisReader;
        private Streams.Writer thisWriter;

        /// <summary>
        /// The index for the physical drive path (only applies to hard disks)
        /// </summary>
        public int DeviceIndex { get; set; }

        /// <summary>
        /// Default Drive class constructor for physical disks
        /// </summary>
        /// <remarks></remarks>
        /// <param name="Index">物理磁盘序号</param>
        public Drive(int Index)
        {
            DriveType = DriveType.HardDisk;
            DeviceIndex = Index;
            length = Length;
            name = Name;
            SecuritySector.Drive = this;
            JoshSector.Drive = this;
        }

        /// <summary>
        /// Drive class constructor for thumb drives
        /// </summary>
        public Drive(string[] PathsIn)
        {
            USBPaths = PathsIn;
            DriveType = DriveType.USB;
            length = Length;
            name = Name;
            SecuritySector.Drive = this;
            JoshSector.Drive = this;
        }

        /// <summary>
        /// Drive class constructor for backups/single-file partitions
        /// </summary>
        public Drive(string PathToFile)
        {
            FilePath = PathToFile;
            DriveType = DriveType.Backup;
            length = Length;
            name = Name;
            SecuritySector.Drive = this;
            JoshSector.Drive = this;
        }

        /// <summary>
        /// 关闭磁盘
        /// </summary>
        public void Close()
        {
            try
            {
                thisStream.Close();
            }
            catch
            {
                throw new Exception("File stream close failure.");
            }

            thisReader = null;
            thisWriter = null;
            thisStream = null;
        }

        /// <summary>
        /// 检查磁盘是否关闭
        /// </summary>
        public bool IsClosed
        {
            get
            {
                if (thisStream == null || thisStream.CanRead == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 打开磁盘
        /// </summary>
        public void Open()
        {
            if (IsClosed == true)
            {
                thisStream = Stream();
            }
        }

        /// <summary>
        /// 检查驱动器是否为合法的FATX驱动器格式
        /// </summary>
        /// <returns>是或者否</returns>
        public bool IsFATXDrive()
        {
            switch (DriveType)
            {
                // HDD类型
                case DriveType.HardDisk:
                    {
                        try
                        {
                            // 创建一个Reader，检查该驱动器类型是否为DEVKIT
                            Streams.Reader sHddReader = Reader();
                            sHddReader.BaseStream.Position = 0;

                            // 如果该驱动器魔数数值为0x00020000，则为DEVKIT类型磁盘
                            if (sHddReader.ReadUInt32() == (uint)DriveMagic.DEVKIT)
                            {
                                // 如果为DEVKIT类型磁盘，计算扇区大小
                                DevPartitionRegions[] regions = DevPartitions();
                                sHddReader.BaseStream.Position = (long)regions[0].Sector * 0x200;

                                // 检查数据分区的魔数数值是否为XTAF
                                if (sHddReader.ReadUInt32() == (uint)DriveMagic.XTAF)
                                {
                                    return IsDev = true;
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                // 按照偏移量找到数据分区
                                sHddReader.BaseStream.Position = (long)Geometry.HDDOffsets.Data;

                                // 检查数据分区的魔数数值是否为XTAF
                                if (sHddReader.ReadUInt32() == (uint)DriveMagic.XTAF)
                                {
                                    return true;
                                }
                                else
                                {

                                }
                            }
                        }
                        catch
                        {
                            throw new Exception("");
                        }
                        break;
                    }
                case DriveType.USB:
                    {
                        try
                        {
                            // 根据偏移量找到数据分区
                            Streams.Reader sUsbReader = Reader();
                            sUsbReader.BaseStream.Position = (long)Geometry.USBOffsets.Data;

                            // 检查数据分区的魔数数值是否为XTAF
                            if (sUsbReader.ReadUInt32() == (uint)DriveMagic.XTAF)
                            {
                                return true;
                            }
                            else
                            {

                            }
                        }
                        catch
                        {
                            throw new Exception("");
                        }
                        break;
                    }
                case DriveType.Backup:
                    {
                        try
                        {
                            Streams.Reader sBackupReader = Reader();
                            sBackupReader.BaseStream.Position = 0;

                            if (sBackupReader.ReadUInt32() == (uint)DriveMagic.DEVKIT)
                            {
                                DevPartitionRegions[] regions = DevPartitions();
                                sBackupReader.BaseStream.Position = (long)regions[0].Sector * 0x200;

                                if (sBackupReader.ReadUInt32() == (uint)DriveMagic.XTAF)
                                {
                                    return IsDev = true;
                                }
                                return IsDev = true;
                            }

                            if (Length > (long)Geometry.HDDOffsets.Data)
                            {
                                // Seek to the data position
                                sBackupReader.BaseStream.Position = (long)Geometry.HDDOffsets.Data;
                            }
                            // Read the magic
                            if (sBackupReader.ReadUInt32() == (uint)DriveMagic.XTAF)
                            {
                                return true;
                            }
                            else
                            {

                            }
                        }
                        catch
                        {
                            throw new Exception("");
                        }
                        break;
                    }
            }
            return false;
        }

        /// <summary>
        /// The type of device
        /// </summary>
        public DriveType DriveType
        {
            get;
            private set;
        }

        /// <summary>
        /// 使用当前磁盘创建一个字节序列
        /// </summary>
        /// <returns>返回当前磁盘的字节序列</returns>
        public Stream Stream()
        {
            if (thisStream == null || IsClosed == true)
            {
                switch (DriveType)
                {
                    case DriveType.HardDisk:
                        {
                            thisStream = new FileStream(VariousFunctions.CreateHandle(DeviceIndex), FileAccess.ReadWrite);
                            break;
                        }
                    case DriveType.USB:
                        {
                            thisStream = new Streams.USBStream(USBPaths, FileMode.Open);
                            break;
                        }
                    case DriveType.Backup:
                        {
                            thisStream = new System.IO.FileStream(FilePath, FileMode.Open);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Drive type not exist.");
                        }
                }
            }
            else
            {
                // Else, try closing the stream, then re-create it, and return it
            }

            return thisStream;
        }

        /// <returns>Streams.Reader applicable to the current drive</returns>
        public Streams.Reader Reader()
        {
            if (thisReader == null)
            {
                thisReader = new Streams.Reader(Stream());
            }
            else
            {

            }

            thisReader.BaseStream.Position = 0;
            return thisReader;
        }

        /// <summary>
        /// Gets a type of System.IO.BinaryWriter for the current drive
        /// </summary>
        /// <returns>Streams.Writer</returns>
        public Streams.Writer Writer()
        {
            if (thisWriter == null)
            {
                thisWriter = new Streams.Writer(Stream());
            }
            else
            {

            }
            thisWriter.BaseStream.Position = 0;
            return thisWriter;
        }

        /// <summary>
        /// Returns the Data0000\Data0001... file paths (only applies to thumb drives)
        /// </summary>
        public string[] USBPaths
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the path to the backup/file passed in the constructor
        /// </summary>
        public string FilePath
        {
            get;
            private set;
        }

        private struct DevPartitionRegions
        {
            public string RegionName;
            public uint Sector;
            public uint PartitionSize;
        }

        List<DevPartitionRegions> dP;/*enetration*/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DevPartitionRegions[] DevPartitions()
        {
            if (dP == null)
            {
                dP = new List<DevPartitionRegions>();

                // Load the regions
                Streams.Reader sReader = Reader();
                sReader.BaseStream.Position = 0;
                byte[] Buffer = sReader.ReadBytes(0x200);
                sReader = new Streams.Reader(new MemoryStream(Buffer));
                sReader.BaseStream.Position = 0x8; // data

                dP.Add(new DevPartitionRegions()
                {
                    RegionName = "Content",
                    Sector = sReader.ReadUInt32(),
                    PartitionSize = sReader.ReadUInt32() * 0x200,
                });
                dP.Add(new DevPartitionRegions()
                {
                    RegionName = "Xbox 360 Dashboard Volume",
                    Sector = sReader.ReadUInt32(),
                    PartitionSize = sReader.ReadUInt32() * 0x200,
                });
            }
            return dP.ToArray();
        }

        Folder[] cachedPartitions = null;
        /// <summary>
        /// Returns an array of valid FATX partitions on the drive
        /// </summary>
        public Folder[] Partitions
        {
            get
            {
                if (cachedPartitions == null)
                {
                    List<Folder> PIList = new List<Folder>();
                    if (DriveType == DriveType.HardDisk)
                    {
                        if (!IsDev)
                        {
                            foreach (Geometry.HDDOffsets e in Enum.GetValues(typeof(Geometry.HDDOffsets)))
                            {
                                if (e == ParrotLibs.Geometry.HDDOffsets.Data || e == Geometry.HDDOffsets.Compatibility || e == ParrotLibs.Geometry.HDDOffsets.System_Cache || e == ParrotLibs.Geometry.HDDOffsets.System_Extended)
                                {
                                    PartitionFunctions FS = new PartitionFunctions(this, (long)e);

                                    if (FS.Magic() == 0x58544146 && (e == Geometry.HDDOffsets.Data || e == ParrotLibs.Geometry.HDDOffsets.Compatibility || e == ParrotLibs.Geometry.HDDOffsets.System_Cache || e == ParrotLibs.Geometry.HDDOffsets.System_Extended))
                                    {
                                        Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                        PI.ClusterSize = FS.ClusterSize();
                                        PI.DataOffset = FS.DataOffset();
                                        PI.FATCopies = FS.FATCopies();
                                        PI.FATOffset = FS.FATOffset;
                                        PI.FATSize = FS.FATSize();
                                        PI.ID = FS.PartitionID();
                                        PI.Magic = FS.Magic();
                                        PI.Name = ((e == ParrotLibs.Geometry.HDDOffsets.System_Cache) ? "System Auxiliary" : ((e == ParrotLibs.Geometry.HDDOffsets.System_Extended) ? "System Extended" : e.ToString()));
                                        PI.Offset = (long)e;
                                        PI.SectorsPerCluster = FS.SectorsPerCluster();
                                        PI.EntrySize = FS.EntrySize;
                                        PI.Size = FS.PartitionSize();
                                        PI.Clusters = FS.Clusters();
                                        PI.RealFATSize = FS.RealFATSize();
                                        Structs.EntryData fData = new ParrotLibs.Structs.EntryData();
                                        fData.StartingCluster = FS.RootDirectoryCluster();
                                        fData.Name = PI.Name;
                                        fData.Flags = 0x10; // Subdirectory flag
                                        Folder f = new Folder(PI, fData, this);
                                        f.FullPath = PI.Name;
                                        PIList.Add(f);
                                    }
                                }
                            }
                        }
                        else
                        {
                            DevPartitionRegions[] partitions = DevPartitions();

                            foreach (DevPartitionRegions p in partitions)
                            {
                                PartitionFunctions FS = new PartitionFunctions(this, (long)p.Sector * 0x200, p.PartitionSize);

                                if (FS.Magic() == 0x58544146)
                                {
                                    Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                    PI.ClusterSize = FS.ClusterSize();
                                    PI.DataOffset = FS.DataOffset();
                                    PI.FATCopies = FS.FATCopies();
                                    PI.FATOffset = FS.FATOffset;
                                    PI.FATSize = FS.FATSize();
                                    PI.ID = FS.PartitionID();
                                    PI.Magic = FS.Magic();
                                    PI.Name = p.RegionName;
                                    PI.Offset = (long)p.Sector * 0x200;
                                    PI.SectorsPerCluster = FS.SectorsPerCluster();
                                    PI.EntrySize = FS.EntrySize;
                                    PI.Size = FS.PartitionSize();
                                    PI.Clusters = FS.Clusters();
                                    PI.RealFATSize = FS.RealFATSize();
                                    Structs.EntryData fData = new ParrotLibs.Structs.EntryData();
                                    fData.StartingCluster = FS.RootDirectoryCluster();
                                    fData.Name = PI.Name;
                                    fData.Flags = 0x10; // Subdirectory flag
                                    Folder f = new Folder(PI, fData, this);
                                    f.FullPath = PI.Name;
                                    PIList.Add(f);
                                }
                            }
                        }
                    }
                    else if (DriveType == DriveType.USB)
                    {
                        foreach (Geometry.USBOffsets e in Enum.GetValues(typeof(Geometry.USBOffsets)))
                        {
                            if (e == Geometry.USBOffsets.Cache)
                            {
                                continue;
                            }
                            PartitionFunctions FS = null;
                            FS = new PartitionFunctions(this, (long)e);

                            if (FS.Magic() == 0x58544146)
                            {
                                Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                PI.ClusterSize = FS.ClusterSize();
                                PI.DataOffset = FS.DataOffset();
                                PI.FATCopies = FS.FATCopies();
                                PI.FATOffset = FS.FATOffset;
                                PI.FATSize = FS.FATSize();
                                PI.ID = FS.PartitionID();
                                PI.Magic = FS.Magic();
                                PI.Name = ((e == Geometry.USBOffsets.aSystem_Aux) ? "System Auxiliary" : ((e == ParrotLibs.Geometry.USBOffsets.aSystem_Extended) ? "System Extended" : e.ToString()));
                                PI.Offset = (long)e;
                                PI.SectorsPerCluster = FS.SectorsPerCluster();
                                PI.EntrySize = FS.EntrySize;
                                PI.Size = FS.PartitionSize();
                                PI.Clusters = FS.Clusters();
                                PI.RealFATSize = FS.RealFATSize();
                                Structs.EntryData fData = new Structs.EntryData();
                                fData.StartingCluster = FS.RootDirectoryCluster();
                                fData.Name = PI.Name;
                                fData.Flags = 0x10; // Subdirectory flag
                                Folder f = new Folder(PI, fData, this);
                                f.FullPath = PI.Name;
                                PIList.Add(f);
                            }
                        }
                        {
                            PartitionFunctions FS = null;
                            if (PIList.Count > 1)
                            {
                                FS = new PartitionFunctions(this, (long)Geometry.USBOffsets.Cache);
                            }
                            else
                            {
                                FS = new PartitionFunctions(this, (long)Geometry.USBOffsets.Cache, (long)Geometry.USBPartitionSizes.Cache_NoSystem);
                            }

                            if (FS.Magic() == 0x58544146)
                            {
                                Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                PI.ClusterSize = FS.ClusterSize();
                                PI.DataOffset = FS.DataOffset();
                                PI.FATCopies = FS.FATCopies();
                                PI.FATOffset = FS.FATOffset;
                                PI.FATSize = FS.FATSize();
                                PI.ID = FS.PartitionID();
                                PI.Magic = FS.Magic();
                                PI.Name = "Cache";
                                PI.Offset = (long)Geometry.USBOffsets.Cache;
                                PI.SectorsPerCluster = FS.SectorsPerCluster();
                                PI.EntrySize = FS.EntrySize;
                                PI.Size = FS.PartitionSize();
                                PI.Clusters = FS.Clusters();
                                PI.RealFATSize = FS.RealFATSize();
                                Structs.EntryData fData = new Structs.EntryData();
                                fData.StartingCluster = FS.RootDirectoryCluster();
                                fData.Name = PI.Name;
                                fData.Flags = 0x10; // Subdirectory flag
                                Folder f = new Folder(PI, fData, this);
                                f.FullPath = PI.Name;
                                PIList.Add(f);
                            }
                        }

                    }
                    else if (DriveType == DriveType.Backup)
                    {
                        if (!IsDev)
                        {
                            if (Length > (long)Geometry.HDDOffsets.Data)
                            {
                                foreach (Geometry.HDDOffsets e in Enum.GetValues(typeof(Geometry.HDDOffsets)))
                                {
                                    if (e == ParrotLibs.Geometry.HDDOffsets.Data || e == ParrotLibs.Geometry.HDDOffsets.Compatibility || e == ParrotLibs.Geometry.HDDOffsets.System_Cache || e == ParrotLibs.Geometry.HDDOffsets.System_Extended)
                                    {
                                        PartitionFunctions FS = new PartitionFunctions(this, (long)e);

                                        if (FS.Magic() == 0x58544146 && (e == ParrotLibs.Geometry.HDDOffsets.Data || e == ParrotLibs.Geometry.HDDOffsets.Compatibility || e == ParrotLibs.Geometry.HDDOffsets.System_Cache || e == ParrotLibs.Geometry.HDDOffsets.System_Extended))
                                        {
                                            Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                            PI.ClusterSize = FS.ClusterSize();
                                            PI.DataOffset = FS.DataOffset();
                                            PI.FATCopies = FS.FATCopies();
                                            PI.FATOffset = FS.FATOffset;
                                            PI.FATSize = FS.FATSize();
                                            PI.ID = FS.PartitionID();
                                            PI.Magic = FS.Magic();
                                            PI.Name = ((e == Geometry.HDDOffsets.System_Cache) ? "System Auxiliary" : ((e == ParrotLibs.Geometry.HDDOffsets.System_Extended) ? "System Extended" : e.ToString()));
                                            PI.Offset = (long)e;
                                            PI.SectorsPerCluster = FS.SectorsPerCluster();
                                            PI.EntrySize = FS.EntrySize;
                                            PI.Size = FS.PartitionSize();
                                            PI.Clusters = FS.Clusters();
                                            PI.RealFATSize = FS.RealFATSize();
                                            Structs.EntryData fData = new ParrotLibs.Structs.EntryData();
                                            fData.StartingCluster = FS.RootDirectoryCluster();
                                            fData.Name = PI.Name;
                                            fData.Flags = 0x10; // Subdirectory flag
                                            Folder f = new Folder(PI, fData, this);
                                            f.FullPath = PI.Name;
                                            PIList.Add(f);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                PartitionFunctions FS = new PartitionFunctions(this, (long)0);

                                if (FS.Magic() == 0x58544146)
                                {
                                    Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                    PI.ClusterSize = FS.ClusterSize();
                                    PI.DataOffset = FS.DataOffset();
                                    PI.FATCopies = FS.FATCopies();
                                    PI.FATOffset = FS.FATOffset;
                                    PI.FATSize = FS.FATSize();
                                    PI.ID = FS.PartitionID();
                                    PI.Magic = FS.Magic();
                                    PI.Name = "Root";
                                    PI.Offset = 0;
                                    PI.SectorsPerCluster = FS.SectorsPerCluster();
                                    PI.EntrySize = FS.EntrySize;
                                    PI.Size = FS.PartitionSize();
                                    PI.Clusters = FS.Clusters();
                                    PI.RealFATSize = FS.RealFATSize();
                                    Structs.EntryData fData = new ParrotLibs.Structs.EntryData();
                                    fData.StartingCluster = FS.RootDirectoryCluster();
                                    fData.Name = PI.Name;
                                    fData.Flags = 0x10; // Subdirectory flag
                                    Folder f = new Folder(PI, fData, this);
                                    f.FullPath = PI.Name;
                                    PIList.Add(f);
                                }
                            }
                        }
                        else
                        {
                            DevPartitionRegions[] partitions = DevPartitions();

                            foreach (DevPartitionRegions p in partitions)
                            {
                                PartitionFunctions FS = new PartitionFunctions(this, (long)p.Sector * 0x200, p.PartitionSize);

                                if (FS.Magic() == 0x58544146)
                                {
                                    Structs.PartitionInfo PI = new Structs.PartitionInfo();
                                    PI.ClusterSize = FS.ClusterSize();
                                    PI.DataOffset = FS.DataOffset();
                                    PI.FATCopies = FS.FATCopies();
                                    PI.FATOffset = FS.FATOffset;
                                    PI.FATSize = FS.FATSize();
                                    PI.ID = FS.PartitionID();
                                    PI.Magic = FS.Magic();
                                    PI.Name = p.RegionName;
                                    PI.Offset = (long)p.Sector * 0x200;
                                    PI.SectorsPerCluster = FS.SectorsPerCluster();
                                    PI.EntrySize = FS.EntrySize;
                                    PI.Size = FS.PartitionSize();
                                    PI.Clusters = FS.Clusters();
                                    PI.RealFATSize = FS.RealFATSize();
                                    Structs.EntryData fData = new ParrotLibs.Structs.EntryData();
                                    fData.StartingCluster = FS.RootDirectoryCluster();
                                    fData.Name = PI.Name;
                                    fData.Flags = 0x10; // Subdirectory flag
                                    Folder f = new Folder(PI, fData, this);
                                    f.FullPath = PI.Name;
                                    PIList.Add(f);
                                }
                            }
                        }
                    }
                    return cachedPartitions = PIList.ToArray();
                }
                return cachedPartitions;
            }
        }



        /// <summary>
        /// Size of the drive (in bytes)
        /// </summary>
        public long Length
        {
            get
            {
                if (length == 0)
                {
                    if (DriveType == DriveType.HardDisk)
                    {
                        API api = new API();
                        var diskGeo = new API.DISK_GEOMETRY();
                        bool result = api.GetDriveGeometry(ref diskGeo, DeviceIndex, VariousFunctions.CreateHandle(DeviceIndex));
                        length = diskGeo.DiskSize;
                    }
                    else if (DriveType == DriveType.USB)
                    {
                        long size = 0;
                        for (int i = 0; i < USBPaths.Length; i++)
                        {
                            size += new System.IO.FileInfo(USBPaths[i]).Length;
                        }
                        length = size;
                    }
                    else
                    {
                        length = Stream().Length;
                    }
                }
                return length;
            }
        }

        /// <summary>
        /// The friendly (GB/MB/KB/byte) size of the drive
        /// </summary>
        public string LengthFriendly
        {
            get
            {
                return VariousFunctions.ByteConversion(Length);
            }
        }

        /// <summary>
        /// Returns true if the hard drive belongs to an XDK
        /// </summary>
        public bool IsDev
        {
            get;
            internal set;
        }

        
        /// <summary>
        /// Gets the name of the drive as assigned by the Xbox/user (Data\name.txt file).  If no such file/partition exists, returns the drive type
        /// </summary>
        public string Name
        {
            get
            {
                if (IsFATXDrive())
                {
                    if (IsDev)
                    {
                        return name = "Dev HDD";
                    }
                    if (name == "")
                    {
                        // Oh no, it's not cached...
                        try
                        {
                            File File = FileFromPath("Data\\name.txt");
                            Streams.Reader io = new Streams.Reader(File.GetStream());
                            io.BaseStream.Position = 2;
                            name = io.ReadUnicodeString((int)io.BaseStream.Length - 2);// +" " + DriveSizeConverted;
                        }
                        catch
                        {
                            // That shit wasn't found... just return the drive type
                            return name = DriveType.ToString();// +" " + DriveSizeConverted;
                        }
                    }
                }
                return name;
            }
        }

        public string CacheFolderPath
        {
            get
            {
                switch (DriveType)
                {
                    case DriveType.HardDisk:
                        return "Data\\Cache";
                    case DriveType.USB:
                        return "Cache";
                    case DriveType.Backup:
                        if (Length > (long)Geometry.HDDOffsets.Data)
                        {
                            return "Data\\Cache";
                        }
                        else
                        {
                            return null;
                        }
                    default:
                        return null;
                }
            }

        }

        List<Structs.CachedTitleName> names = null;
        /// <summary>
        /// Gets the Xbox-cached title names
        /// </summary>
        /// <returns>
        /// uint TitleID
        /// string TitleName
        /// </returns>
        public Structs.CachedTitleName[] CachedTitleNames()
        {
            if (names == null)
            {
                names = new List<Structs.CachedTitleName>();
                if (CacheFolderPath == null)
                {
                    return names.ToArray();
                }
                try
                {
                    Folder Cache = FolderFromPath(CacheFolderPath);
                    foreach (File file in Cache.Files())
                    {
                        if (file.Name.StartsWith(Geometry.CacheFilePrefixes.GetPrefix(Geometry.Prefixes.TitleName)))
                        {
                            names.AddRange(CachedFromFile(file));
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Could not get cache folder");
                }
            }
            return names.ToArray();
        }

        /// <summary>
        /// Gets the title names that correspond with title ID's from a file
        /// </summary>
        /// <returns>List of title ID's with their accurate title names</returns>
        private Structs.CachedTitleName[] CachedFromFile(File f)
        {
            List<Structs.CachedTitleName> tnl = new List<Structs.CachedTitleName>();
            int BlockLength = 0x34;
            Streams.Reader r = new Streams.Reader(f.GetStream());
            for (int i = 0, Previous = 0; i < r.BaseStream.Length / BlockLength; i++, Previous += 0x34)
            {
                // Set the position of the stream
                r.BaseStream.Position = Previous;
                // Create the new CachedTitleName struct
                Structs.CachedTitleName TN = new Structs.CachedTitleName();
                // Read the title ID
                TN.ID = r.ReadUInt32();
                // Read the name
                TN.Name = r.ReadCString();
                // Add that to the list
                tnl.Add(TN);
            }
            return tnl.ToArray();
        }

        /// <summary>
        /// Calculates the total size of all of the partitions - amount of clusters that have been used amongst all partitions
        /// </summary>
        /// <returns>Remaining storage in bytes</returns>
        public long RemainingSpace()
        {
            // Size up to the first user-partition (non-cache, non-gay)
            long size = 0;
            Console.WriteLine(Partitions.Length);
            for (int i = 0; i < Partitions.Length; i++)
            {
                
                size += new PartitionFunctions(this, Partitions[i]).GetFreeSpace();
            }
            return size;
        }
        /// <summary>
        /// Calculates the size of all of the partitions together (usable clusters)
        /// </summary>
        /// <returns>Size (in bytes) of the amount of data that can be held in all of the partitions</returns>
        public long PartitionSizeTotal()
        {
            long size = 0;
            for (int i = 0; i < Partitions.Length; i++)
            {
                size += Partitions[i].PartitionInfo.Size - (Partitions[i].PartitionInfo.DataOffset - Partitions[i].PartitionInfo.Offset);
            }
            return size;
        }

        public File FileFromPath(string Path)
        {
            string PathBrowsed = "";
            string[] Split = Path.Split('\\');
            Folder Current = null;
            foreach (Folder f in Partitions)
            {
                if (f.Name.ToLower() == Split[0].ToLower())
                {
                    Current = f;
                    PathBrowsed += f.Name;
                    break;
                }
            }
            if (Current == null)
            {
                throw new Exception("Partition not found: " + Split[0]);
            }

            // Get the folders and shit
            for (int i = 1; i < Split.Length - 1; i++)
            {
                bool Found = false;
                foreach (Folder f in Current.Folders())
                {
                    if (f.Name.ToLower() == Split[i].ToLower())
                    {
                        Found = true;
                        Current = f;
                        PathBrowsed += f.Name;
                        break;
                    }
                }
                if (!Found)
                {
                    throw new Exception("Part of path not found: " + PathBrowsed + Split[i]);
                }
            }

            // Get the file
            foreach (File f in Current.Files())
            {
                if (f.Name.ToLower() == Split[Split.Length - 1].ToLower())
                {
                    return f;
                }
            }
            throw new Exception("File not found! " + Path);
        }

        public Folder FolderFromPath(string Path)
        {
            string PathBrowsed = "";
            string[] Split = Path.Split('\\');
            Folder Current = null;
            foreach (Folder f in Partitions)
            {
                if (f.Name.ToLower() == Split[0].ToLower())
                {
                    Current = f;
                    PathBrowsed += f.Name;
                    break;
                }
            }
            if (Current == null)
            {
                throw new Exception("Partition not found: " + Split[0]);
            }

            if (Split.Length == 2 && Split[1] == "")
            {
                return Current;
            }
            // Get the folders and shit
            for (int i = 1; i < Split.Length; i++)
            {
                bool Found = false;
                foreach (Folder f in Current.Folders())
                {
                    if (f.Name.ToLower() == Split[i].ToLower())
                    {
                        Found = true;
                        Current = f;
                        PathBrowsed += f.Name;
                        break;
                    }
                }
                if (!Found)
                {
                    throw new Exception("Part of path not found: " + PathBrowsed + Split[i]);
                }
            }
            return Current;
        }

        public Folder CreateDirectory(string Path)
        {
            string[] Split = Path.Split('\\');
            Folder Current = null;
            foreach (Folder f in Partitions)
            {
                if (f.Name.ToLower() == Split[0].ToLower())
                {
                    Current = f;
                    break;
                }
            }
            if (Current == null)
            {
                throw new Exception("Partition not found: " + Split[0]);
            }

            // Get the folders and shit
            for (int i = 1; i < Split.Length; i++)
            {
                bool Found = false;
                foreach (Folder f in Current.Folders())
                {
                    if (f.Name.ToLower() == Split[i].ToLower())
                    {
                        Found = true;
                        Current = f;
                        break;
                    }
                }
                if (!Found)
                {
                    Current.CreateNewFolder(Split[i]);
                    Current = Current.Folders()[Current.Folders().Length - 1];
                }
            }
            return Current;
        }

        public event Structs.OnEntryEvent EntryWatcher;

        internal virtual void OnEntryEvent(ref Structs.EntryEventArgs e)
        {
            if (EntryWatcher != null)
            {
                EntryWatcher(ref e);
            }
        }

        internal bool EntryWatcherNull
        {
            get
            {
                if (EntryWatcher == null)
                {
                    return true;
                }
                return false;
            }
        }

        public long GetPartitionRemainingStorage(Folder Partition)
        {
            return new PartitionFunctions(Partition).GetFreeSpace();
        }

        public DateTime PartitionTimeStamp(Structs.PartitionInfo PI)
        {
            return VariousFunctions.DateTimeFromFATInt((ushort)((PI.ID & ~0xFFFF) >> 8), (ushort)PI.ID);
        }

        /// <summary>
        /// Returns an array of free blocks based off of the number of blocks needed
        /// </summary>
        public uint[] GetFreeBlocks(Folder Partition, int blocksNeeded, uint StartBlock, long end, bool SecondLoop)
        {
            int Clustersize = 0x10000;
            uint Block = StartBlock;
            if (end == 0)
            {
                end = Partition.PartitionInfo.FATOffset + Partition.PartitionInfo.FATSize;
            }
            List<uint> BlockList = new List<uint>();
            // Create our reader for the drive
            Streams.Reader br = Reader();
            // Create our reader for the memory stream
            Streams.Reader mr = null;
            for (long i = VariousFunctions.DownToNearest200(VariousFunctions.BlockToFATOffset(StartBlock, Partition)); i < end; i += Clustersize)
            {
                //Set our position to i
                br.BaseStream.Position = i;
                byte[] buffer = new byte[0];
                if ((end - i) < Clustersize)
                {
                    buffer = VariousFunctions.ReadBytes(ref br, end - i);
                }
                else
                {
                    //Read our buffer
                    buffer = br.ReadBytes(Clustersize);
                }
                try
                {
                    mr.Close();
                }
                catch { }
                mr = new Streams.Reader(new System.IO.MemoryStream(buffer));
                //Re-open our binary reader using the buffer/memory stream
                for (int j = 0; j < buffer.Length; j += (int)Partition.PartitionInfo.EntrySize, Block += (uint)Partition.PartitionInfo.EntrySize)
                {
                    mr.BaseStream.Position = j;
                    //If we've gotten all of our requested blocks...
                    if (BlockList.Count == blocksNeeded)
                    {
                        //Close our reader -> break the loop
                        mr.Close();
                        break;
                    }
                    //Read the next block entry
                    byte[] reading = mr.ReadBytes((int)Partition.PartitionInfo.EntrySize);
                    //For each byte in our reading
                    for (int k = 0; k < reading.Length; k++)
                    {
                        //If the byte isn't null (if the block isn't open)
                        if (reading[k] != 0x00)
                        {
                            //Break
                            break;
                        }
                        //If we've reached the end of the array, and the last byte
                        //is 0x00, then the block is free
                        if (k == reading.Length - 1 && reading[k] == 0x00)
                        {
                            //Do some maths to get the block numbah
                            long fOff = Partition.PartitionInfo.FATOffset;
                            long blockPosition = (long)i + j;
                            uint block = (uint)(blockPosition - fOff) / (uint)Partition.PartitionInfo.EntrySize;
                            BlockList.Add(block);
                        }
                    }
                }
                //We're putting in one last check so that we don't loop more than we need to
                if (BlockList.Count == blocksNeeded)
                {
                    break;
                }
            }
            //If we found the required amount of free blocks - return our list
            if (BlockList.Count == blocksNeeded)
            {
                return BlockList.ToArray();
            }
            //If we didn't find the amount of blocks required, but we started from a
            //block other than the first one...
            if (BlockList.Count < blocksNeeded && SecondLoop == false)
            {
                BlockList.AddRange(GetFreeBlocks(Partition, blocksNeeded - BlockList.Count, 1, VariousFunctions.DownToNearest200(VariousFunctions.BlockToFATOffset(StartBlock, Partition)), true));
                return BlockList.ToArray();
            }
            //We didn't find the amount of free blocks required, meaning we're ref of
            //disk space
            if (BlockList.Count != blocksNeeded)
            {
                throw new Exception("Out of Xbox 360 hard disk space");
            }
            return BlockList.ToArray();
        }

        public bool DriveHasSecuritySector()
        {
            if (DriveType == DriveType.USB)
            {
                return false;
            }
            Streams.Reader r = Reader();
            r.BaseStream.Position = 0x2000;
            if (r.ReadByte() == 0x20)
            {
                return true;
            }
            return false;
        }

        public bool DriveHasJoshSector()
        {
            if (DriveType == DriveType.USB)
            {
                return false;
            }
            Streams.Reader r = Reader();
            r.BaseStream.Position = 0x800;
            if (r.ReadUInt32() == 0x4A6F7368)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class SecuritySector
        {
            internal static Drive Drive
            {
                get;
                set;
            }
            public static string SerialNumber()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x2000;
                string Value = r.ReadASCIIString(0x14);
                return Value;
            }

            public static string FirmwareRevision()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x2014;
                string Value = r.ReadASCIIString(0x8);
                return Value;
            }

            public static string ModelNumber()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x201C;
                string Value = r.ReadASCIIString(0x28);
                return Value;
            }

            public static uint Sectors()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x2058;
                uint Value = r.ReadUInt32();
                return Value;
            }

            public static System.Drawing.Image MicrosoftLogo()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x2200;
                uint Length = r.ReadUInt32();
                System.Drawing.Image Value = System.Drawing.Image.FromStream(new System.IO.MemoryStream(r.ReadBytes((int)Length)));
                return Value;
            }
        }

        /// <summary>
        /// Josh扇区
        /// </summary>
        public static class JoshSector
        {
            /// <summary>
            /// 
            /// </summary>
            internal static Drive Drive
            {
                get;
                set;
            }
            public static uint Magic()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x800;
                uint Value = r.ReadUInt32();
                return Value;
            }

            public static byte[] ConsoleCertificate()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0x804;
                byte[] Value = r.ReadBytes(0x1A8);
                return Value;
            }

            public static uint[] FooterInts()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0xA70;
                List<uint> R = new List<uint>();
                for (int i = 0; i < 3; i++)
                {
                    R.Add(r.ReadUInt32());
                }
                return R.ToArray();
            }

            public static uint FirstPlayedGame()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0xA7C;
                uint Value = r.ReadUInt32();
                return Value;
            }

            public static uint SecondPlayedGame()
            {
                Streams.Reader r = Drive.Reader();
                r.BaseStream.Position = 0xA80;
                uint Value = r.ReadUInt32();
                return Value;
            }
        }
    }
}
