using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ParrotLibs.Structs
{
    /// <summary>
    /// 数据项数据
    /// </summary>
    public struct EntryData
    {
        /// <summary>
        /// 数据项名称长度(Byte)
        /// </summary>
        public byte NameSize;
        /// <summary>
        /// 数据项标志
        /// </summary>
        public byte Flags;
        /// <summary>
        /// 数据项名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 数据项开始簇(Block)
        /// </summary>
        public UInt32 StartingCluster;
        /// <summary>
        /// 数据项大小(文件夹大小为0x00)
        /// </summary>
        public UInt32 Size;
        /// <summary>
        /// 数据项创建日期
        /// </summary>
        public UInt16 CreationDate;
        /// <summary>
        /// 数据项创建时间
        /// </summary>
        public UInt16 CreationTime;
        /// <summary>
        /// 数据项访问日期
        /// </summary>
        public UInt16 AccessDate;
        /// <summary>
        /// 数据项访问时间
        /// </summary>
        public UInt16 AccessTime;
        /// <summary>
        /// 数据项修改日期
        /// </summary>
        public UInt16 ModifiedDate;
        /// <summary>
        /// 数据项修改时间
        /// </summary>
        public UInt16 ModifiedTime;
        /// <summary>
        /// 数据项偏移量
        /// </summary>
        public long EntryOffset;
    }

    public struct STFSInfo
    {
        /// <summary>
        /// STFS包的ICON
        /// </summary>
        public System.Drawing.Image ContentImage;
        /// <summary>
        /// STFS包中游戏ICON
        /// </summary>
        public System.Drawing.Image TitleImage;
        /// <summary>
        /// STFS包生成时控制台识别符
        /// </summary>
        public byte[] ConsoleID;
        /// <summary>
        /// STFS包生成时设备的识别符
        /// </summary>
        public byte[] DeviceID;
        /// <summary>
        /// STFS包的魔数值(Magic Number)
        /// </summary>
        public byte[] Magic;
        /// <summary>
        /// STFS包创建时配置文件ID(取决于transfer flags);
        /// </summary>
        public byte[] ProfileID;
        /// <summary>
        /// STFS包中游戏或应用的名称
        /// </summary>
        public string TitleName;
        /// <summary>
        /// STFS包的名称
        /// </summary>
        public string ContentName;
        /// <summary>
        /// STFS包中游戏或应用的识别符
        /// </summary>
        public uint TitleID;
    }

    /// <summary>
    /// 提供给定分区的规定信息
    /// </summary>
    public struct PartitionInfo
    {
        /// <summary>
        /// 对应分区魔数的十进制数值(Magic Number)
        /// </summary>
        [Category("Header"), DisplayName("Partition Magic"), Description("The partition magic displayed in decimal")]
        public uint Magic
        {
            get;
            internal set;
        }

        /// <summary>
        /// 对应分区魔数的十六进制数值
        /// </summary>
        [Category("Header"), DisplayName("Partition Magic"), Description("The partition magic displayed in hex")]
        public string MagicAsString
        {
            get
            {
                return "0x" + Magic.ToString("X2");
            }
        }

        /// <summary>
        /// 对应分区簇大小的十进制数值
        /// </summary>
        [Category("Header"), DisplayName("Cluster Size"), Description("The size of each cluster in the partition in decimal")]
        /// <summary>
        /// Cluster size
        /// </summary>
        public long ClusterSize
        {
            get;
            internal set;
        }

        /// <summary>
        /// 对应分区簇大小的十六进制数值
        /// </summary>
        [Category("Header"), DisplayName("Cluster Size"), Description("The size of each cluster in the partition in hex")]
        public string ClusterSizeAsString
        {
            get
            {
                return "0x" + ClusterSize.ToString("X");
            }
        }

        /// <summary>
        /// 对应分区识别符的十进制数值
        /// </summary>
        [Category("Header"), DisplayName("Partition ID"), Description("The partition identifier in decimal")]
        public uint ID
        {
            get;
            internal set;
        }

        /// <summary>
        /// 对应分区识别符的十六进制数值
        /// </summary>
        [Category("Header"), DisplayName("Partition ID"), Description("The partition identifier in hex")]
        public string IDAsString
        {
            get
            {
                return "0x" + ID.ToString("X2");
            }
        }

        /// <summary>
        /// 每个簇对应扇区的十进制数值
        /// </summary>
        [Category("Header"), DisplayName("Sectors Per Cluster"), Description("The number of sectors per cluster in decimal")]
        public uint SectorsPerCluster
        {
            get;
            internal set;
        }

        /// <summary>
        /// 每个簇对应扇区的十六进制数值
        /// </summary>
        [Category("Header"), DisplayName("Sectors Per Cluster"), Description("The number of sectors per cluster in hex")]
        public string SectorsPerClusterAsString
        {
            get
            {
                return "0x" + SectorsPerCluster.ToString("X");
            }
        }

        /// <summary>
        /// 文件分区表(File Allocation Table)备份数
        /// </summary>
        [Category("FAT"), DisplayName("FAT Copies"), Description("The number of file allocation table copies")]
        public uint FATCopies
        {
            get;
            internal set;
        }

        /// <summary>
        /// 文件分区表大小的十进制数值
        /// </summary>
        [Category("FAT"), DisplayName("FAT Size"), Description("The file allocation table size in decimal")]

        public long FATSize
        {
            get;
            internal set;
        }

        /// <summary>
        /// 文件分区表大小的十六进制数值
        /// </summary>
        [Category("FAT"), DisplayName("FAT Size"), Description("The file allocation table size in hex")]
        public string FATSizeAsString
        {
            get
            {
                return "0x" + FATSize.ToString("X");
            }
        }

        /// <summary>
        /// 文件分区表大小(bytes/KB/GB)
        /// </summary>
        [Category("FAT"), DisplayName("FAT Size"), Description("The file allocation table size in bytes/KB/GB")]
        public string FATSizeAsFriendly
        {
            get
            {
                return VariousFunctions.ByteConversion(FATSize);
            }
        }

        /// <summary>
        /// 对应分区数据起始偏移量的十进制数值
        /// </summary>
        [Category("Data"), DisplayName("Data Region Start"), Description("The location in which data starts in decimal")]
        public long DataOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// 对应分区数据起始偏移量的十六进制数值
        /// </summary>
        [Category("Data"), DisplayName("Data Region Start"), Description("The location in which data starts in hex")]
        public string DataOffsetAsString
        {
            get
            {
                return "0x" + DataOffset.ToString("X");
            }
        }

        /// <summary>
        /// 文件分区表起始偏移量的十进制数值
        /// </summary>
        [Category("FAT"), DisplayName("FAT Offset"), Description("The file allocation table starting offset in decimal")]
        public long FATOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// 文件分区表起始偏移量的十六进制数值
        /// </summary>
        [Category("FAT"), DisplayName("FAT Offset"), Description("The file allocation table starting offset in hex")]
        public string FATOffsetAsString
        {
            get
            {
                return "0x" + FATOffset.ToString("X");
            }
        }

        /// <summary>
        /// 对应分区大小的十进制数值
        /// </summary>
        [Category("Partition"), DisplayName("Partition Size"), Description("The partition size in decimal")]
        public long Size
        {
            get;
            internal set;
        }

        /// <summary>
        /// 对应分区大小的十六进制数值
        /// </summary>
        [Category("Partition"), DisplayName("Partition Size"), Description("The partition size in hex")]
        public string SizeAsString
        {
            get
            {
                return "0x" + Size.ToString("X");
            }
        }

        /// <summary>
        /// 分区大小(bytes/KB/MB/GB)
        /// </summary>
        [Category("Partition"), DisplayName("Partition Size"), Description("The partition size in bytes/KB/MB/GB")]
        public string SizeFriendly
        {
            get
            {
                return VariousFunctions.ByteConversion(Size);
            }
        }

        /// <summary>
        /// 分区偏移量的十进制数值
        /// </summary>
        [Category("Partition"), DisplayName("Partition Offset"), Description("The partition offset in decimal")]
        public long Offset
        {
            get;
            internal set;
        }

        /// <summary>
        /// 分区偏移量的十六进制数值
        /// </summary>
        [Category("Partition"), DisplayName("Partition Offset"), Description("The partition offset in hex")]
        public string OffsetAsString
        {
            get
            {
                return "0x" + Offset.ToString("X");
            }
        }

        /// <summary>
        /// 分区名称
        /// </summary>
        [Category("Partition"), DisplayName("Partition Name"), Description("The partition name")]
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// 数据项大小(bytes)
        /// </summary>
        [Category("FAT"), DisplayName("Chainmap Size"), Description("The size (in bytes) of a chainmap entry")]
        public int EntrySize
        {
            get;
            internal set;
        }

        /// <summary>
        /// 分区中簇数量的十进制数值
        /// </summary>
        [Category("Partition"), DisplayName("Clusters"), Description("The number of clusters in this partition in decimal")]
        public uint Clusters
        {
            get;
            internal set;
        }

        /// <summary>
        /// 分区中簇数量的十六进制数值
        /// </summary>
        [Category("Partition"), DisplayName("Clusters"), Description("The number of clusters in this partition in hex")]
        public string ClustersAsString
        {
            get
            {
                return "0x" + Clusters.ToString("X");
            }
        }

        /// <summary>
        /// 文件分区表实际大小
        /// </summary>
        [Category("FAT"), DisplayName("Don't mind this"), Description("It's dumb.")]
        public long RealFATSize
        {
            get;
            internal set;
        }
    }

    /// <summary>
    /// 队列
    /// </summary>
    public struct Queue
    {
        public Folder Folder;
        public string Path;
        public bool Writing;
    }

    /// <summary>
    /// 已存在数据项
    /// </summary>
    public struct ExistingEntry
    {
        // The entry that already exists
        public Entry Existing;
        // The path to the entry they tried writing
        public string NewPath;
    }

    /// <summary>
    /// 释放的数据项
    /// </summary>
    public struct FreeEntry
    {
        public bool UsingDeletedEntry;
        public EntryData NewEntryData;
    }

    /// <summary>
    /// 被缓存的标题名称
    /// </summary>
    public struct CachedTitleName
    {
        public uint ID;
        public string Name;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct WriteResult
    {
        // The entry that either exists, or was written
        public Entry Entry;
        public bool CouldNotWrite;
        public string ConflictingEntryPath;
        public Entry AttemptedEntryToMove;
    }

    /// <summary>
    /// 文件操作
    /// </summary>
    public struct FileAction
    {
        public int Progress { get; internal set; }
        public int MaxValue { get; internal set; }
        public string FullPath { get; internal set; }
        public bool Cancel;
    }
    
    public delegate void FileActionChanged(ref FileAction Progress);

    /// <summary>
    /// 文件夹操作
    /// </summary>
    public struct FolderAction
    {
        public int Progress { get; internal set; }
        public int MaxValue { get; internal set; }
        public bool Cancel { get; set; }
        public string CurrentFile { get; internal set; }
        public string CurrentFilePath { get; internal set; }
    }

    public delegate void FolderActionChanged(ref FolderAction Progress);

    /// <summary>
    /// 
    /// </summary>
    public struct EntryEventArgs
    {
        public string FullParentPath { get; internal set; }
        public Folder ParentFolder { get; internal set; }
        public Entry ModifiedEntry { get; internal set; }
        public bool Deleting { get; internal set; }
    }

    public delegate void OnEntryEvent(ref EntryEventArgs e);
}
