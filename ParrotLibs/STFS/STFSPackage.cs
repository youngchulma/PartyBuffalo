using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParrotLibs.STFS
{
    class STFSPackage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aInStream"></param>
        public STFSPackage(System.IO.Stream aInStream)
        {
            PackageStream = aInStream;
            if (IsSTFSPackage() != true)
            {
                Close();
                throw new Exception("File is not a valid STFS package!");
            }
            else
            {
                // File is valid STFS package!
            }
        }

        /// <summary>
        /// 如果文件是STFS包，获取游戏的ICON
        /// </summary>
        /// <returns>返回游戏的ICON</returns>
        public System.Drawing.Image TitleIcon()
        {
            int sSize = 0;

            // Get a new reader for this file
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.TitleImageSize;
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.TitleImage;
            sSize = sReader.ReadInt32();

            try
            {
                return System.Drawing.Image.FromStream(new System.IO.MemoryStream(sReader.ReadBytes(sSize)));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 如果文件是STFS包，获取包的ICON(非游戏ICON)
        /// </summary>
        /// <returns>返回STFS包的ICON</returns>
        public System.Drawing.Image ContentIcon()
        {
            int sSize = 0;

            // Get a new reader for this file
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.ContentImageSize;
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.ContentImage;
            sSize = sReader.ReadInt32();

            try
            {
                return System.Drawing.Image.FromStream(new System.IO.MemoryStream(sReader.ReadBytes(sSize)));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 计算文件魔数(Magic Number)，确认文件的类型
        /// CON/LIVE/PIRS/NIGR 类型
        /// </summary>
        public bool IsSTFSPackage()
        {
            // 1010 0000 0000 0000
            if (PackageStream.Length >= 0xA000)
            {
                uint sVal = 0;

                Streams.Reader sReader = new Streams.Reader(PackageStream);
                sReader.BaseStream.Position = 0;
                // 512 Bytes
                byte[] Buffer = sReader.ReadBytes(0x200);
                
                sReader = new Streams.Reader(new System.IO.MemoryStream(Buffer));
                sVal = sReader.ReadUInt32();

                switch (sVal)
                {
                    // CON ‭ 1010 0011 0100 1111 0100 1110 0010 0000‬
                    case 0x434F4E20:
                        return true;
                    // LIVE ‭0100 1100 0100 1001 0101 0110 0100 0101‬
                    case 0x4C495645:
                        return true;
                    // PIRS ‭0101 0000 0100 1001 0101 0010 0101 0011‬
                    case 0x50495253:
                        return true;
                    // NIGR
                    default:
                        return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 如果文件是STFS包，获取包的名称
        /// </summary>
        /// <returns>返回STFS包的名称</returns>
        public string ContentName()
        {
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.DisplayName;

            return sReader.ReadUnicodeString(0x80);
        }

        /// <summary>
        /// 如果文件是STFS包，获取包中游戏名称
        /// </summary>
        /// <returns>返回STFS包所属游戏名称</returns>
        public string TitleName()
        {
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.TitleName;

            return sReader.ReadUnicodeString(0x80);
        }

        /// <summary>
        /// 如果文件是STFS包，获取包中游戏识别符
        /// </summary>
        /// <returns>返回STFS包中游戏识别符</returns>
        public uint TitleID()
        {
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.TitleID;

            return sReader.ReadUInt32();
        }

        /// <summary>
        /// 如果文件是STFS包，获取包所有者配置文件ID
        /// </summary>
        /// <returns>返回STFS包所有者配置文件ID</returns>
        public byte[] ProfileID()
        {
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.ProfileID;

            return sReader.ReadBytes(0x8);
        }

        /// <summary>
        /// 如果文件是STFS包，获取该包生成时设备ID(HDD/USB)
        /// </summary>
        /// <returns>返回STFS包生成时设备ID</returns>
        public byte[] DeviceID()
        {
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.DeviceID;

            return sReader.ReadBytes(0x14);
        }

        /// <summary>
        /// 如果文件是STFS包，获取该包生成时的识别符
        /// </summary>
        /// <returns>返回STFS包生成时的识别符</returns>
        public byte[] ConsoleID()
        {
            Streams.Reader sReader = new Streams.Reader(PackageStream);
            sReader.BaseStream.Position = (long)Geometry.STFSOffsets.ConsoleID;

            return sReader.ReadBytes(0x5);
        }

        private System.IO.Stream PackageStream
        {
            get;
            set;
        }

        public void Close()
        {
            PackageStream.Flush();
            PackageStream.Close();
        }
    }
}
