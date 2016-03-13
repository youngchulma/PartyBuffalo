using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CLKsFATXLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using Microsoft.WindowsAPICodePack.Dialogs;
using Extensions;

namespace Party_Buffalo.Forms
{
    public partial class EntryAction : Form
    {
        TaskbarManager tm;
        System.Threading.Thread t;
        Folder Parent;
        Entry[] Entries;
        Method mMethod;
        string OutPath;
        string[] Paths;
        string[] EntriesToSkip = new string[0];
        volatile bool Cancel;
        Drive xDrive;
        bool Windows7 = false;
        bool Aero = false;
        TaskDialog mTaskDialog;
        bool Timer = true;
        CLKsFATXLib.Structs.Queue[] MultiInject;

        public enum Method
        {
            Extract,
            Inject,
            Delete,
            ExtractSS,
            ExtractJ,
            Backup,
            Restore,
            Move,
        }

        public EntryAction(Entry[] Entries, Method method, string Path)
        {
            InitializeComponent();
            if (Environment.OSVersion.Version.Build >= 7600)
            {
                Windows7 = true;
            }
            if (Environment.OSVersion.Version.Build >= 6000)
            {
                Aero = true;
            }
            if (Windows7)
            {
                tm = TaskbarManager.Instance;
            }
            this.HandleCreated += new EventHandler(EntryAction_HandleCreated);
            this.FormClosing += new FormClosingEventHandler(EntryAction_FormClosing);
            mMethod = method;
            OutPath = Path;
            this.Entries = Entries;
        }

        public EntryAction(CLKsFATXLib.Structs.Queue[] mi)
        {
            InitializeComponent();
            if (Environment.OSVersion.Version.Build >= 7600)
            {
                Windows7 = true;
            }
            if (Environment.OSVersion.Version.Build >= 6000)
            {
                Aero = true;
            }
            if (Windows7)
            {
                tm = TaskbarManager.Instance;
            }
            this.HandleCreated += new EventHandler(EntryAction_HandleCreated);
            this.FormClosing += new FormClosingEventHandler(EntryAction_FormClosing);
            MultiInject = mi;
        }

        public EntryAction(Drive xDrive, Method method, string Path)
        {
            InitializeComponent();
            if (Environment.OSVersion.Version.Build >= 7600)
            {
                Windows7 = true;
            }
            if (Environment.OSVersion.Version.Build >= 6000)
            {
                Aero = true;
            }
            if (Windows7)
            {
                tm = TaskbarManager.Instance;
            }
            this.HandleCreated += new EventHandler(EntryAction_HandleCreated);
            this.FormClosing += new FormClosingEventHandler(EntryAction_FormClosing);
            mMethod = method;
            OutPath = Path;
            this.xDrive = xDrive;
        }

        public EntryAction(Entry[] Entries, Method method, string[] FoldersToSkip, string Path)
        {
            InitializeComponent();
            if (Environment.OSVersion.Version.Build >= 7600)
            {
                Windows7 = true;
            }
            if (Environment.OSVersion.Version.Build >= 6000)
            {
                Aero = true;
            }
            if (Windows7)
            {
                tm = TaskbarManager.Instance;
            }
            this.HandleCreated += new EventHandler(EntryAction_HandleCreated);
            this.FormClosing += new FormClosingEventHandler(EntryAction_FormClosing);
            mMethod = method;
            OutPath = Path;
            this.Entries = Entries;
            this.EntriesToSkip = FoldersToSkip;
        }

        public EntryAction(string[] Paths, Folder Parent, Method method)
        {
            InitializeComponent();
            if (Environment.OSVersion.Version.Build >= 7600)
            {
                Windows7 = true;
            }
            if (Environment.OSVersion.Version.Build >= 6000)
            {
                Aero = true;
            }
            if (Windows7)
            {
                tm = TaskbarManager.Instance;
            }
            this.HandleCreated += new EventHandler(EntryAction_HandleCreated);
            this.FormClosing += new FormClosingEventHandler(EntryAction_FormClosing);
            mMethod = method;
            this.Paths = Paths;
            this.Parent = Parent;
        }

        public EntryAction(string[] FilePaths, Drive Drive, Method method)
        {
            InitializeComponent();
            if (Environment.OSVersion.Version.Build >= 7600)
            {
                Windows7 = true;
            }
            if (Environment.OSVersion.Version.Build >= 6000)
            {
                Aero = true;
            }
            if (Windows7)
            {
                tm = TaskbarManager.Instance;
            }
            this.HandleCreated += new EventHandler(EntryAction_HandleCreated);
            this.FormClosing += new FormClosingEventHandler(EntryAction_FormClosing);
            mMethod = method;
            this.Paths = FilePaths;
            xDrive = Drive;
        }

        void EntryAction_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Media.SystemSounds.Asterisk.Play();
            if (Windows7)
            {
                tm.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            if (Entries != null)
            {
                for (int i = 0; i < Entries.Length; i++)
                {
                    if (Entries[i].IsFolder)
                    {
                        ResetFolderActions((Folder)Entries[i]);
                    }
                }
            }
        }

        void ResetFolderActions(Folder folder)
        {
            folder.ResetFolderAction();
            foreach (Folder f in folder.Folders())
            {
                ResetFolderActions(f);
            }
        }

        void EntryAction_HandleCreated(object sender, EventArgs e)
        {
            if (Aero)
            {
                mTaskDialog = new TaskDialog();
            }
            System.Threading.ThreadStart ts = delegate
            {
#if TRACE
                try
                {
#endif
                //while (true)//(!this.IsHandleCreated || !progressBar1.IsHandleCreated || !label1.IsHandleCreated || !lPercent.IsHandleCreated || !button1.IsHandleCreated)
                //{
                //    try
                //    {
                //        this.Invoke((MethodInvoker)delegate { });
                //        progressBar1.Invoke((MethodInvoker)delegate { });
                //        label1.Invoke((MethodInvoker)delegate { });
                //        lPercent.Invoke((MethodInvoker)delegate { });
                //        button1.Invoke((MethodInvoker)delegate { });
                //        break;
                //    }
                //    catch(Exception E) { Application.DoEvents(); }
                //}
                if (xDrive != null && mMethod == Method.Backup || mMethod == Method.ExtractJ || mMethod == Method.ExtractSS || mMethod == Method.Restore)
                {
                    switch (mMethod)
                    {
                        case Method.Backup:
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.Icon = Properties.Resources.Add;
                            });
                            CLKsFATXLib.Streams.Reader r = xDrive.Reader();
                            CLKsFATXLib.Streams.Writer w = new CLKsFATXLib.Streams.Writer(new System.IO.FileStream(OutPath, System.IO.FileMode.Create));
                            int ReadLength = 0x200;
                            if (xDrive.Length % 0x100000 == 0)
                            {
                                ReadLength = 0x100000;
                            }
                            else if (xDrive.Length % 0x40000 == 0)
                            {
                                ReadLength = 0x40000;
                            }
                            else if (xDrive.Length % 0x10000 == 0)
                            {
                                ReadLength = 0x10000;
                            }
                            else if (xDrive.Length % 0x5000 == 0)
                            {
                                ReadLength = 0x5000;
                            }
                            for (int i = 0; i < xDrive.Length / ReadLength; i++)
                            {
                                if (Cancel)
                                {
                                    break;
                                }
                                w.Write(r.ReadBytes(ReadLength));
                                p_ProgressBar.Invoke((MethodInvoker)delegate
                                {
                                    try
                                    {
                                        p_ProgressBar.Maximum = (int)(xDrive.Length / ReadLength);
                                        p_ProgressBar.Value = (i + 1);
                                        if (Windows7)
                                        {
                                            tm.SetProgressValue(p_ProgressBar.Value, p_ProgressBar.Maximum);
                                        }
                                    }
                                    catch { }
                                });
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.Text = "Backing Up Drive";
                                });
                                l_Percent.Invoke((MethodInvoker)delegate
                                {
                                    l_Percent.Text = (((decimal)(i + 1) / (decimal)(xDrive.Length / ReadLength)) * 100).ToString("#") + "%";
                                });
                            }
                            w.Close();
                            break;
                        case Method.ExtractSS:
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.Icon = Properties.Resources.Add;
                            });
                            //Create our io for the drive
                            CLKsFATXLib.Streams.Reader io = xDrive.Reader();
                            //Go to the location of the security sector
                            io.BaseStream.Position = 0x2000;
                            //Create our ref io for the file
                            CLKsFATXLib.Streams.Writer bw = new CLKsFATXLib.Streams.Writer(new System.IO.FileStream(OutPath, System.IO.FileMode.Create));
                            //Read the sector.  The size is an estimation, since I have no idea how big it really is
                            bw.Write(io.ReadBytes(0xE00));
                            //Close our io
                            bw.Close();
                            break;
                        case Method.ExtractJ:
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.Icon = Properties.Resources.Add;
                            });
                            //Create our io for the drive
                            CLKsFATXLib.Streams.Reader io2 = xDrive.Reader();
                            //Go to the location of the security sector
                            io2.BaseStream.Position = 0x800;
                            //Create our ref io for the file
                            CLKsFATXLib.Streams.Writer bw2 = new CLKsFATXLib.Streams.Writer(new System.IO.FileStream(OutPath, System.IO.FileMode.Create));
                            //Read the sector.  The size is an estimation, since I have no idea how big it really is
                            bw2.Write(io2.ReadBytes(0x400));
                            //Close our io
                            bw2.Close();
                            break;
                        case Method.Restore:
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.Icon = Properties.Resources.Remove;
                            });
                            if (MessageBox.Show("WARNING: Restoring a drive that does not match your current one can cause for data to not be read correctly by the Xbox 360, or for other unforseen problems!  Please make sure you know what you're doing before continuing.  Are you sure you want to continue?", "WARNING AND STUFF", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                if (MessageBox.Show("This is your last chance to stop!  Are you POSITIVE you want to continue?", "Last Chance!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    CLKsFATXLib.Streams.Reader r2 = new CLKsFATXLib.Streams.Reader(new System.IO.FileStream(OutPath, System.IO.FileMode.Open));
                                    CLKsFATXLib.Streams.Writer w2 = xDrive.Writer();
                                    int ReadLength2 = 0x200;
                                    if (xDrive.Length % 0x4000 != 0)
                                    {
                                        ReadLength2 = 0x4000;
                                    }
                                    else
                                    {
                                        for (int i = 0x300000; i > 0x200; i -= 0x1000)
                                        {
                                            if (xDrive.Length % i == 0)
                                            {
                                                ReadLength2 = i;
                                                break;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < xDrive.Length / ReadLength2; i++)
                                    {
                                        if (Cancel)
                                        {
                                            break;
                                        }
                                        w2.Write(r2.ReadBytes(ReadLength2));
                                        p_ProgressBar.Invoke((MethodInvoker)delegate
                                        {
                                            try
                                            {
                                                p_ProgressBar.Maximum = (int)(xDrive.Length / ReadLength2);
                                                p_ProgressBar.Value = (i + 1);
                                                if (Windows7)
                                                {
                                                    tm.SetProgressValue(p_ProgressBar.Value, p_ProgressBar.Maximum);
                                                }
                                            }
                                            catch { }
                                        });
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            this.Text = "Restoring Drive";
                                        });
                                        l_Percent.Invoke((MethodInvoker)delegate
                                        {
                                            l_Percent.Text = (((decimal)(i + 1) / (decimal)(xDrive.Length / ReadLength2)) * 100).ToString("#") + "%";
                                        });
                                    }
                                    r2.Close();
                                }
                            }
                            break;
                    }
                }
                else
                {
                    Folder ParentFolder = null;
                    this.Invoke((MethodInvoker)delegate
                    {
                        ParentFolder = Parent;
                    });

                    switch (mMethod)
                    {
                        // 提取游戏文件
                        case Method.Extract:
                            {
#if DEBUG
                                System.Diagnostics.Stopwatch sStopwatch = new System.Diagnostics.Stopwatch();
                                if (Timer)
                                {
                                    sStopwatch.Start();
                                }
#endif
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.Icon = Properties.Resources.Add;
                                });
                                foreach (Entry sEntry in Entries)
                                {
                                    // 提取的元素不是文件夹
                                    if (sEntry.IsFolder == false)
                                    {
                                        ((File)sEntry).FileAction += new CLKsFATXLib.Structs.FileActionChanged(EntryAction_FileAction);
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            this.Text = sEntry.FullPath;
                                        });

                                        l_Percent.Invoke((MethodInvoker)delegate
                                        {
                                            l_Percent.Text = sEntry.Name;
                                        });

                                        // Check to see if we're batch-extracting...
                                        if (Entries.Length == 1)
                                        {
                                            ((File)sEntry).Extract(OutPath);
                                        }
                                        else
                                        {
                                            ((File)sEntry).Extract(OutPath + "\\" + sEntry.Name);
                                        }
                                    }
                                    else
                                    {
                                        ((Folder)sEntry).FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                                        ((Folder)sEntry).Extract(OutPath, EntriesToSkip);
                                    }
                                    if (Cancel)
                                    {
                                        break;
                                    }
                                }
#if DEBUG
                                if (Timer)
                                {
                                    sStopwatch.Stop();
                                    MessageBox.Show(string.Format("{0}:{1}:{2}", sStopwatch.Elapsed.Minutes, sStopwatch.Elapsed.Seconds, sStopwatch.Elapsed.Milliseconds));
                                }
#endif
                                break;
                            }
                            // 删除游戏文件
                        case Method.Delete:
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.Icon = Properties.Resources.Remove;
                                });

                                foreach (Entry sEntry in Entries)
                                {
                                    if (Cancel == true)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        // 要删除的是文件夹
                                        if (sEntry.IsFolder == true)
                                        {
                                            Folder sCurrent = ((Folder)sEntry);
                                            sCurrent.ResetFolderAction();
                                            sCurrent.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                                            sCurrent.Delete();
                                        }
                                        // 要删除的是文件
                                        else
                                        {
                                            this.Invoke((MethodInvoker)delegate 
                                            {
                                                this.Text = sEntry.FullPath;
                                            });

                                            l_Percent.Invoke((MethodInvoker)delegate
                                            {
                                                l_Percent.Text = sEntry.Name;
                                            });

                                            File sCurrent = ((File)sEntry);
                                            sCurrent.FileAction += new CLKsFATXLib.Structs.FileActionChanged(EntryAction_FileAction);
                                            sCurrent.Delete();
                                        }
                                    }
                                }

                                break;
                            }
                            // 添加新游戏
                        case Method.Inject:
                            {
                                #if DEBUG
                                System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
                                if (Timer)
                                {
                                    sw2.Start();
                                }
                                #endif
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.Icon = Properties.Resources.Add;
                                });

                                if (ParentFolder != null)
                                {
                                    ParentFolder.ResetFolderAction();
                                    ParentFolder.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                                    List<CLKsFATXLib.Structs.ExistingEntry> Existing = new List<CLKsFATXLib.Structs.ExistingEntry>();
                                    foreach (string sPath in Paths)
                                    {
                                        if (Cancel == true)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            // 导入的是文件夹
                                            if (VariousFunctions.IsFolder(sPath) == true)
                                            {
                                                // 不合并，不覆盖
                                                Existing.AddRange(ParentFolder.InjectFolder(sPath, false, false));
                                            }
                                            // 导入的是文件
                                            else
                                            {
                                                ParentFolder.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                                                CLKsFATXLib.Structs.WriteResult sWriteResult = ParentFolder.CreateNewFile(sPath);

                                                // 如果不能写入，则检查是否存在
                                                if (sWriteResult.CouldNotWrite == true)
                                                {
                                                    CLKsFATXLib.Structs.ExistingEntry sExistEntry = new CLKsFATXLib.Structs.ExistingEntry();
                                                    sExistEntry.Existing = sWriteResult.Entry;
                                                    sExistEntry.NewPath = sPath;
                                                    Existing.Add(sExistEntry);
                                                }
                                            }
                                        }
                                    }

                                    DoExisting(Existing);
                                }
                                else
                                {
                                    List<CLKsFATXLib.Structs.ExistingEntry> Existing = new List<CLKsFATXLib.Structs.ExistingEntry>();
                                    foreach (string s in Paths)
                                    {
                                        string Path = "";
                                        try
                                        {  
                                            // XBOX360 1代磁盘类型
                                            Path = VariousFunctions.GetFATXPath(s);
                                        }
                                        catch (Exception x)
                                        {
                                            ExceptionHandler(x);
                                            continue;
                                        }
                                        Folder thisFolder = xDrive.CreateDirectory("Data\\" + Path);
                                        thisFolder.ResetFolderAction();
                                        thisFolder.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                                        if (Cancel)
                                        {
                                            break;
                                        }
                                        if (VariousFunctions.IsFolder(s))
                                        {
                                            ExceptionHandler(new Exception("Can not write folder as STFS package (silly error wording)"));
                                            continue;
                                        }
                                        else
                                        {
                                            thisFolder.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                                            CLKsFATXLib.Structs.WriteResult wr = thisFolder.CreateNewFile(s);
                                            if (wr.CouldNotWrite)
                                            {
                                                CLKsFATXLib.Structs.ExistingEntry ex = new CLKsFATXLib.Structs.ExistingEntry();
                                                ex.Existing = wr.Entry;
                                                ex.NewPath = s;
                                                Existing.Add(ex);
                                            }
                                        }
                                    }

                                    DoExisting(Existing);
                                }
#if DEBUG
                                if (Timer)
                                {
                                    sw2.Stop();
                                    MessageBox.Show(string.Format("{0}:{1}:{2}", sw2.Elapsed.Minutes, sw2.Elapsed.Seconds, sw2.Elapsed.Milliseconds));
                                }
#endif
                                break;
                            }
                        case Method.Move:
                            {
                                List<CLKsFATXLib.Structs.WriteResult> Results = new List<CLKsFATXLib.Structs.WriteResult>();
                                foreach (Entry sEntry in Entries)
                                {
                                    CLKsFATXLib.Structs.WriteResult sWriteResult = sEntry.Move(OutPath);
                                    if (sWriteResult.CouldNotWrite == true)
                                    {
                                        Results.Add(sWriteResult);
                                    }
                                    else
                                    {
                                        // DO NOTHING
                                    }
                                }

                                break;
                            }
                    }
                }
                this.Invoke((MethodInvoker)delegate { this.Close(); });
#if TRACE
                }
                catch (Exception x)
                {
                    ExceptionHandler(x);
                    this.Invoke((MethodInvoker)delegate { this.Close(); });
                }
#endif
            };
            t = new System.Threading.Thread(ts);
            t.Start();
        }

        void ExceptionHandler(Exception x)
        {
            if (!Aero)
            {
                MessageBox.Show("An exception was thrown: " + x.Message + "\r\n\r\nPress CTRL + C to copy the stack trace:\r\n" + x.StackTrace);
            }
            else
            {
                tm.SetProgressState(TaskbarProgressBarState.Error);
                this.Invoke((MethodInvoker)delegate
                {
                    TaskDialog td = new TaskDialog();
                    td.Caption = "Unhandled Exception";
                    td.InstructionText = "An Unhandled Exception was Thrown";
                    td.Text = string.Format("An exception was thrown: {0}\r\n\r\nIf this appears to be a bug, please email me at clkxu5@gmail.com with the details below", x.Message);
                    td.DetailsCollapsedLabel = "Details";
                    td.DetailsExpandedLabel = "Details";
                    td.DetailsExpandedText = x.StackTrace;

                    TaskDialogButton Copy = new TaskDialogButton("Copy", "Copy Details to Clipboard");
                    Copy.Click += (o, f) => { this.Invoke((MethodInvoker)delegate { Clipboard.SetDataObject(x.Message + "\r\n\r\n" + x.StackTrace, true, 10, 200); }); };

                    TaskDialogButton Close = new TaskDialogButton("Close", "Close");
                    Close.Click += (o, f) => { td.Close(); };

                    td.Controls.Add(Copy);
                    td.Controls.Add(Close);
                    td.ShowDialog(this.Handle);
                });
            }
        }

        private void DoExisting(List<CLKsFATXLib.Structs.ExistingEntry> aExistList)
        {
            if (aExistList.Count == 0)
            {
                return;
            }

            // 文件、文件夹、文件包含文件夹、文件夹包含文件
            List<CLKsFATXLib.Structs.ExistingEntry> sFiles = new List<CLKsFATXLib.Structs.ExistingEntry>();
            List<CLKsFATXLib.Structs.ExistingEntry> sFolders = new List<CLKsFATXLib.Structs.ExistingEntry>();
            List<CLKsFATXLib.Structs.ExistingEntry> sFilesWithFolders = new List<CLKsFATXLib.Structs.ExistingEntry>();
            List<CLKsFATXLib.Structs.ExistingEntry> sFoldersWithFiles = new List<CLKsFATXLib.Structs.ExistingEntry>();
            
            MergedPaths = new List<CLKsFATXLib.Structs.ExistingEntry>();
            goto _Sort;
        _Sort:
            {
                // 对ExistList中的元素进行分类
                foreach (CLKsFATXLib.Structs.ExistingEntry sEntry in aExistList)
                {
                    // 元素中的内容是文件夹，并且该元素是文件夹
                    if (sEntry.Existing.IsFolder && VariousFunctions.IsFolder(sEntry.NewPath))
                    {
                        sFolders.Add(sEntry);
                    }
                    // 元素中的内容是文件夹，并且该元素是文件
                    else if (sEntry.Existing.IsFolder && !VariousFunctions.IsFolder(sEntry.NewPath))
                    {
                        sFilesWithFolders.Add(sEntry);
                    }
                    // 元素中的内容是文件，并且该元素是文件夹
                    else if (!sEntry.Existing.IsFolder && VariousFunctions.IsFolder(sEntry.NewPath))
                    {
                        sFoldersWithFiles.Add(sEntry);
                    }
                    // 文件
                    else
                    {
                        sFiles.Add(sEntry);
                    }
                }

                // 分类后，对ExistList进行初始化
                aExistList = new List<CLKsFATXLib.Structs.ExistingEntry>();
            }

            bool Delete = false;

            for (int i = 0; i < sFolders.Count; i++)
            {
                if (Cancel)
                {
                    return;
                }
                if (Windows7)
                {
                    tm.SetProgressValue(1, 1);
                    tm.SetProgressState(TaskbarProgressBarState.Error);
                }
                DialogResult dr = DialogResult.Ignore;
                bool Checked = false;

                // VISTA 之前版本
                if (Aero == false)
                {
                    InjectDialog sInjectDialog = new InjectDialog(sFolders[i], sFolders.Count - 1);
                    CheckForIllegalCrossThreadCalls = false;
                    sInjectDialog.ShowDialog(this.Owner);
                    Checked = sInjectDialog.checkBox1.Checked;
                    CheckForIllegalCrossThreadCalls = true;
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        mTaskDialog.Caption = "该内容已存在";
                        mTaskDialog.InstructionText = "无法对下列路径进行写操作 \"" + sFolders[i].NewPath + "\"";
                        if (sFolders[i].Existing.IsFolder == true)
                        {
                            mTaskDialog.Text = "因为在 \"" + sFolders[i].Existing.Parent.FullPath + "\" 路径下，";
                            mTaskDialog.Text += "有相同的 \"" + sFolders[i].Existing.Name + "\" 文件夹存在！";
                            mTaskDialog.Text += "是否合并该文件夹？";
                        }
                        else
                        {
                            mTaskDialog.Text = "因为在 \"" + sFolders[i].Existing.Parent.FullPath + "\" 路径下，";
                            mTaskDialog.Text += "有相同的 \"" + sFolders[i].Existing.Name + "\" 文件存在！";
                            mTaskDialog.Text += "是否将旧文件覆盖？";
                        }

                        mTaskDialog.Icon = TaskDialogStandardIcon.Warning;
                        mTaskDialog.FooterCheckBoxChecked = false;
                        mTaskDialog.FooterCheckBoxText = "全部应用? (目标数:" + sFolders.Count + ")";
                        mTaskDialog.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;
                        if (mTaskDialog.ShowDialog(this.Handle) == TaskDialogResult.Yes)
                        {
                            dr = DialogResult.Yes;
                        }
                        Checked = (bool)mTaskDialog.FooterCheckBoxChecked;
                    });
                }

                // If they want to merge the folders
                if (dr == DialogResult.Yes)
                {
                    if (Windows7)
                    {
                        tm.SetProgressState(TaskbarProgressBarState.Normal);
                    }
                    // If they want to do all of them...
                    if (Checked)
                    {
                        foreach (var existing in sFolders)
                        {
                            aExistList.AddRange(((Folder)existing.Existing).Parent.InjectFolder(existing.NewPath, true, Delete));
                            MergedPaths.Add(existing);
                        }
                        sFolders = new List<CLKsFATXLib.Structs.ExistingEntry>();
                        break;
                    }
                    else
                    {
                        foreach (System.IO.DirectoryInfo di in new System.IO.DirectoryInfo(sFolders[i].NewPath).GetDirectories())
                        {
                            aExistList.AddRange(((Folder)sFolders[i].Existing).InjectFolder(di.FullName, false, Delete));
                        }
                        foreach (System.IO.FileInfo fi in new System.IO.DirectoryInfo(sFolders[i].NewPath).GetFiles())
                        {
                            CLKsFATXLib.Structs.WriteResult wr = ((Folder)sFolders[i].Existing).CreateNewFile(fi.FullName);
                            CLKsFATXLib.Structs.ExistingEntry exe = new CLKsFATXLib.Structs.ExistingEntry();
                            exe.Existing = wr.Entry;
                            exe.NewPath = fi.FullName;
                            aExistList.Add(exe);
                        }

                        //Existing.AddRange(((Folder)Folders[i].Existing).Parent.InjectFolder(Folders[i].NewPath, false, Delete));
                        MergedPaths.Add(sFolders[i]);
                        sFolders.RemoveAt(i);
                        i--;
                    }
                }
                else if (Checked)
                {
                    sFolders = new List<CLKsFATXLib.Structs.ExistingEntry>();
                }
            }

            if (aExistList.Count != 0)
            {
                goto _Sort;
            }

            for (int i = 0; i < sFiles.Count; i++)
            {
                if (Cancel)
                {
                    return;
                }
                // If we don't even have to show a dialog...
                if (Delete)
                {
                    sFiles[i].Existing.Parent.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                    ((File)sFiles[i].Existing).Delete();
                    sFiles[i].Existing.Parent.CreateNewFile(sFiles[i].NewPath);
                }
                else
                {
                    if (Windows7)
                    {
                        tm.SetProgressValue(1, 1);
                        tm.SetProgressState(TaskbarProgressBarState.Error);
                    }
                    DialogResult dr = DialogResult.Ignore;
                    bool Checked = false;
                    if (!Aero)
                    {
                        Forms.InjectDialog id = new InjectDialog(sFiles[i], sFiles.Count - 1);
                        CheckForIllegalCrossThreadCalls = false;
                        id.ShowDialog(this.Owner);
                        Checked = id.checkBox1.Checked;
                        CheckForIllegalCrossThreadCalls = true;
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            mTaskDialog.Caption = "File Already Exists";
                            mTaskDialog.InstructionText = "Cannot Write File \"" + sFiles[i].NewPath + "\"";
                            mTaskDialog.Text = "A file named \"" + sFiles[i].Existing.Name + "\" already exists in the directory \"" + sFiles[i].Existing.Parent.FullPath + "\".  Would you like to overwrite the currently existing file to write the new file?";
                            mTaskDialog.FooterCheckBoxChecked = false;
                            mTaskDialog.FooterCheckBoxText = "Do this for all current items (" + sFiles.Count + ")";
                            mTaskDialog.Icon = TaskDialogStandardIcon.Error;
                            mTaskDialog.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;
                            if (mTaskDialog.ShowDialog(this.Handle) == TaskDialogResult.Yes)
                            {
                                dr = DialogResult.Yes;
                            }
                            Checked = (bool)mTaskDialog.FooterCheckBoxChecked;
                        });
                    }
                    if (dr == DialogResult.Yes)
                    {
                        if (Windows7)
                        {
                            tm.SetProgressState(TaskbarProgressBarState.Normal);
                        }
                        if (Checked)
                        {
                            Delete = true;
                            sFiles[i].Existing.Parent.FolderAction += new CLKsFATXLib.Structs.FolderActionChanged(EntryAction_FolderAction);
                            ((File)sFiles[i].Existing).Delete();
                            sFiles[i].Existing.Parent.CreateNewFile(sFiles[i].NewPath);
                            sFiles.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            ((File)sFiles[i].Existing).Delete();
                            sFiles[i].Existing.Parent.CreateNewFile(sFiles[i].NewPath);
                            sFiles.RemoveAt(i);
                            i--;
                        }
                    }
                    else if (Checked)
                    {
                        sFiles = new List<CLKsFATXLib.Structs.ExistingEntry>();
                    }
                }
            }

            if (aExistList.Count != 0)
            {
                goto _Sort;
            }

            if (sFilesWithFolders.Count > 0 || sFoldersWithFiles.Count > 0)
            {
                Forms.Existing exForm = new Existing(sFilesWithFolders, sFoldersWithFiles);
                CheckForIllegalCrossThreadCalls = false;
                exForm.Show(this.Owner);
                CheckForIllegalCrossThreadCalls = true;
            }
        }

        public List<CLKsFATXLib.Structs.ExistingEntry> MergedPaths
        {
            get;
            private set;
        }

        void EntryAction_FolderAction(ref CLKsFATXLib.Structs.FolderAction Progress)
        {
            int sCurValue = Progress.Progress;
            int sMaxValue = Progress.MaxValue;

            string sFileName = Progress.CurrentFile;
            string sFilePath = Progress.CurrentFilePath;
            if (sCurValue == 0 && sMaxValue == 0 && sFileName == null && sFilePath == null && Progress.Cancel == true)
            {
                Progress.Cancel = Cancel;
            }
            else
            {
                Progress.Cancel = Cancel;
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Text = sFilePath;
                        if (sFileName != null)
                        {
                            l_Percent.Text = sFileName;
                        }
                        else if (sFilePath != null)
                        {
                            l_Percent.Text = sFilePath.Remove(0, sFilePath.LastIndexOf('\\') + 1);
                        }

                        lPercent.Text = EntryAction_ShowPercent(sCurValue, sMaxValue);
                        
                        p_ProgressBar.Maximum = sMaxValue;
                        p_ProgressBar.Value = sCurValue;

                        if (Windows7)
                        {
                            tm.SetProgressValue(sCurValue, sMaxValue);
                        }
                    });
                }
                catch (Exception e) { }
            }
        }

        private void EntryAction_FileAction(ref CLKsFATXLib.Structs.FileAction Progress)
        {
            try
            {
                int sCurValue = Progress.Progress;
                int sMaxValue = Progress.MaxValue;
                Progress.Cancel = Cancel;

                lPercent.Invoke((MethodInvoker)delegate 
                {
                    lPercent.Text = EntryAction_ShowPercent(sCurValue, sMaxValue);
                });

                p_ProgressBar.Invoke((MethodInvoker)delegate 
                {
                    p_ProgressBar.Maximum = sMaxValue;
                    p_ProgressBar.Value = sCurValue;
                    if (Windows7)
                    {
                        tm.SetProgressValue(sCurValue, sMaxValue);
                    }
                });
            }
            catch { }
        }


        private string EntryAction_ShowPercent(int aCurValue, int aMaxValue)
        {
            // 初始值为0，或者Max太大，都不显示百分比
            string sPercent;
            if (aCurValue == 0 || ((aCurValue / aMaxValue) * 100).ToString("#") == "0")
            {
                sPercent = "";
            }
            else
            {
                sPercent = (aCurValue / aMaxValue * 100).ToString("#") + "%";
            }

            return sPercent;
        }
        private void b_Cancel_Click(object sender, EventArgs e)
        {
            Cancel = true;
        }
    }
}
