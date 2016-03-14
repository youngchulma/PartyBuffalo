
namespace CLKsFATXLib.Geometry
{
    /* Some important kernel functions that i'll keep here...
     * -> FatxProcessBootSector (processes the partition header, determines if the partition is stable
     * -> SataDiskAuthenticateDevice (checks the security sector to make sure it's an OEM drive)
     * -> SataCreateDevkitPartitions (formats a drive for dev use)
     */
    /// <summary>
    /// STFS包偏移量
    /// </summary>
    public enum STFSOffsets
    {
        TitleID = 0x360,
        ConsoleID = 0x36c,
        DeviceID = 0x3FD,
        DisplayName = 0x411,
        TitleName = 0x1691,
        ProfileID = 0x371,
        ContentImage = 0x171A,
        ContentImageSize = 0x1712,
        TitleImageSize = 0x1716,
        TitleImage = 0x571A,
    }

    /// <summary>
    /// XBOX支持的USB存储相关偏移量
    /// </summary>
    public enum USBOffsets
    {
        /// <summary>
        /// USB存储
        /// </summary>
        aSystem_Aux = 0x8115200,
        /// <summary>
        /// USB存储
        /// </summary>
        aSystem_Extended = 0x12000400,
        /// <summary>
        /// USB存储系统缓存偏移量
        /// </summary>
        Cache = 0x8000400,
        /// <summary>
        /// USB存储系统数据偏移量
        /// </summary>
        Data = 0x20000000,
    }

    /// <summary>
    /// USB磁盘分区大小
    /// </summary>
    public enum USBPartitionSizes
    {
        /// <summary>
        /// ???
        /// </summary>
        Cache = 0x47FF000,//0x4000000,//0x12000400,//0x47FF000,
        /// <summary>
        /// ???
        /// </summary>
        Cache_NoSystem = 0x4000000,
        /// <summary>
        /// 
        /// </summary>
        System_Aux = 0x8000000,
        /// <summary>
        /// 
        /// </summary>
        System_Extended = 0xDFFFC00,
    }

    /// <summary>
    /// NAND偏移量
    /// </summary>
    public enum NANDOffsets
    {
        /// <summary>
        /// 
        /// </summary>
        System = 0x3190800,
        /// <summary>
        /// 
        /// </summary>
        Cache = 0x3CE8800,
        /// <summary>
        /// 
        /// </summary>
        Data = 0x1E600300,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum Flags : byte
    {
        /// <summary>
        /// The file or directory is read-only. 
        /// Applications can read the file but cannot write to it or delete it. 
        /// In the case of a directory, applications cannot delete it. 
        /// FATX does not support read-only files.
        /// </summary>
        ReadOnly,
        Hidden,
        System,
        Volume,
        /// <summary>
        /// This attribute identifies a directory.
        /// </summary>
        Directory,
        /// <summary>
        ///  The file or directory is an archive file or directory. 
        ///  Applications use this attribute to mark files for backup or removal. 
        /// </summary>
        Archive,
        /// <summary>
        /// 以下保留，未使用
        /// </summary>
        Device,
        Unused,
        Deleted = 0xE5,
    }

    /// <summary>
    /// 数据项偏移量
    /// </summary>
    public enum EntryOffsets : int
    {
        /// <summary>
        /// 文件名大小的偏移量
        /// </summary>
        NameSize = 0x0,
        /// <summary>
        /// 文件标识的偏移量
        /// </summary>
        Flags = 0x1,
        /// <summary>
        /// 文件名的偏移量
        /// </summary>
        FileName = 0x2,
        /// <summary>
        /// 起始簇的偏移量
        /// </summary>
        StartingCluster = 0x2C,
        /// <summary>
        /// 文件大小的偏移量
        /// </summary>
        Size = 0x30,
        /// <summary>
        /// 文件生成日期的偏移量
        /// </summary>
        CreationDate = 0x34,
        /// <summary>
        /// 文件生成时间的偏移量
        /// </summary>
        CreationTime = 0x36,
        /// <summary>
        /// 文件访问日期的偏移量
        /// </summary>
        AccessDate = 0x38,
        /// <summary>
        /// 文件访问时间的偏移量
        /// </summary>
        AccessTime = 0x3A,
        /// <summary>
        /// 文件修改日期的偏移量
        /// </summary>
        ModifiedDate = 0x3C,
        /// <summary>
        /// 文件修改时间的偏移量
        /// </summary>
        ModifiedTime = 0x3E,
    }

    /// <summary>
    /// HDD相关的长度
    /// </summary>
    public enum HDDLengths : long
    {
        /// <summary>
        /// 系统缓存分区长度
        /// </summary>
        SystemCache = 0x80000000,
        /// <summary>
        /// 游戏缓存分区长度
        /// </summary>
        GameCache = 0xA0E30000,
        /// <summary>
        /// 兼容性分区长度
        /// </summary>
        Compatibility = 0x10000000,
        /// <summary>
        /// ?????
        /// </summary>
        System_Cache = 0xCE30000,
        /// <summary>
        /// ?????
        /// </summary>
        System_Extended = 0x8000000,
        /// <summary>
        /// FATX允许的最大文件长度(4G)
        /// </summary>
        MaxFileSize = 0x100000000,
        /// <summary>
        /// 扇区长度
        /// </summary>
        SectorSize = 0x200,
    }

    /// <summary>
    /// DEVKIT磁盘信息
    /// </summary>
    public enum DevOffsets : long
    {
        /// <summary>
        /// DEVKIT扇区偏移量
        /// </summary>
        DEVKIT_ = 0xB6600000,
        /// <summary>
        /// Content扇区偏移量
        /// </summary>
        Content = 0xC6600000,
    }

    /// <summary>
    /// XBOX支持的HDD磁盘偏移量
    /// </summary>
    public enum HDDOffsets : long
    {
        /// <summary>
        /// 数据分区的偏移量
        /// </summary>
        Data = 0x130EB0000,
        /// <summary>
        /// Josh分区的偏移量
        /// </summary>
        Josh = 0x800,
        /// <summary>
        /// 安全扇区的偏移量
        /// </summary>
        SecuritySector = 0x2000,
        /// <summary>
        /// 系统缓存分区的偏移量
        /// </summary>
        SystemCache = 0x80000,
        /// <summary>
        /// 游戏缓存分区的偏移量
        /// </summary>
        GameCache = 0x80080000,
        /// <summary>
        /// ?????
        /// </summary>
        System_Cache = 0x10C080000,
        /// <summary>
        /// ?????
        /// </summary>
        System_Extended = 0x118EB0000,
        /// <summary>
        /// 兼容性分区的偏移量
        /// </summary>
        Compatibility = 0x120EB0000,
    }

    /// <summary>
    /// 前缀枚举
    /// </summary>
    public enum Prefixes
    {
        XlfsUploader,
        NuiHiveSetting,
        NuiTroubleShooter,
        NuiBiometric,
        NuiSession,
        ValidCert,
        CertStorage,
        AvatarGamerTile,
        ProfileSettings,
        QosHistory,
        MessengerBuddies,
        GameTile,
        TitleUpdate,
        TitleName,
        Tickets,
        SystemUpdate,
        SPA,
        GamerTile,
        GameInvite,
        GamerTag,
        FriendMuteList,
        DashboardApp,
        CustomGamerTile,
        AchievementTile,
    }

    /// <summary>
    /// 缓存文件前缀
    /// </summary>
    public static class CacheFilePrefixes
    {
        /// <summary>
        /// 前缀集合
        /// </summary>
        public static string[] CachePrefixes = 
        {
            "XL", "NH", "TS", "NB", "NS", "VC",
            "CA", "AV", "PS", "QH", "MB", "TT",
            "TU", "TN", "TK", "SU", "SP", "GT",
            "GI", "GA", "FM", "DA", "CT", "AT",
        };

        /// <summary>
        /// 前缀名集合
        /// </summary>
        public static string[] PrefixNames =
        {
            "XLFS Uploader", "NUI Hive Setting", "NUI Troubleshooter", "NUI Biometric", "NUI Session", "Valid Cert",
            "Cert Storage", "Avatar Gamer Tile", "Profile Settings", "QoS History", "Messenger Buddies", "Game Tile",
            "Title Update", "Title Name", "Tickets", "System Update", "SPA", "Gamer Tile",
            "Game Invite", "Gamertag", "Friend Mute List", "Dashboard App", "Custom Gamer Tile", "Achievement Tile",
        };

        /// <summary>
        /// 获取前缀
        /// </summary>
        /// <param name="Prefix">前缀枚举类型</param>
        /// <returns></returns>
        public static string GetPrefix(Prefixes Prefix)
        {
            return CachePrefixes[(int)Prefix];
        }

        /// <summary>
        /// 获取前缀名
        /// </summary>
        /// <param name="Prefix">前缀枚举类型</param>
        /// <returns></returns>
        public static string GetPrefixName(Prefixes Prefix)
        {
            return PrefixNames[(int)Prefix];
        }
    }
}
