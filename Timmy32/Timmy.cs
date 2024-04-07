using FP_CLOCKLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Timmy32
{
    class Timmy
    {
        private bool isConnected { get; set; }
        private int _machineNo = 1;
        private FP_CLOCKClass client;


        public Timmy()
        {
            client = new FP_CLOCKClass();
        }
        public bool ConnectByIp(string ip, int port, int password = 0, int machineNo = 1)
        {
       
            _machineNo = machineNo;
            var isSet = client.SetIPAddress(ref ip, port, password);
            if (!isSet)
                return false;
            var isConnected = client.OpenCommPort(_machineNo);

            return isConnected;

        }
        
        public bool ConnectByUsb(int machineNo = 1)
        {
            _machineNo = machineNo;
            var isSet = client.IsUSB = true;
            if (!isSet)
                return false;
            var isConnected = client.OpenCommPort(_machineNo);

            return isConnected;

        }


       
        public bool DeleteUser(long userId)
        {
            
            if(!IsAI())
                return client.DeleteEnrollData(_machineNo, (int)userId,1,12);
            
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());

            return client.DeleteUserInfoLongID(_machineNo,EnrollIDobj);
        }
        
        public bool DeletePassword(long userId)
        {
            var username = GetName(userId);

            var userInfo = GetUserInfo(userId);

            if (userInfo == null)
                return false;

            userInfo.Password = 0;
            var bret = SetUserInfo(userId, userInfo);

            return bret;
        }

        public bool SetUserVerificationMode(long userId, int mode)
        {
            var userInfo = GetUserInfo(userId);

            if (userInfo == null)
                return false;

            userInfo.UserCtrl = mode;
            var ret = SetUserInfo(userId, userInfo);

            return ret;
        }
        public int GetUserVerificationMode(long userId)
        {
            var userInfo = GetUserInfo(userId);

            if (userInfo == null)
                return -1;

            return userInfo.UserCtrl;
        }
        
        private bool SetUserInfo(long userId, UserInfo userInfo)
        {
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());
            object obj = new System.Runtime.InteropServices.VariantWrapper(userInfo.Name);

            
            var bret = client.SetUserInfoLongID(_machineNo,  ref EnrollIDobj,  ref obj,userInfo.Password,
                userInfo.Card,userInfo.PostID,userInfo.Privilege,userInfo.Enabled,userInfo.ShiftID,userInfo.ZoneID,
                userInfo.GroupID,userInfo.UserCtrl,userInfo.StartTime,userInfo.EndTime,userInfo.BirthDay);

            return bret;
        }

        public bool DeleteFinger(long userId, int fingerIndex)
        {

            if (!IsAI())
            {
                return client.DeleteEnrollData(_machineNo, (int)userId, 1, fingerIndex);
            }
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());

            return client.DeleteFPDataLongID(_machineNo, EnrollIDobj, fingerIndex);
        }
        public void DisConnect()
        {
            
            client.CloseCommPort();
        }


        public bool DisableDevice()
        {
            return client.EnableDevice(_machineNo, 0);
        }

        public bool EnableDevice()
        {
            return client.EnableDevice(_machineNo, 1);
        }

        public void SetDeviceTime()
        {
            client.SetDeviceTime(_machineNo);
        }

        private void setCapacity(DeviceCapacity deviceCapacity)
        {
            deviceCapacity.LogCapacity = 500 * 1000;
            
            var pcode = GetProductCode();
            if (pcode.ToLower().StartsWith("ai810"))
            {
                deviceCapacity.UserCapacity = 50000;
            }
            else if (pcode.ToLower().StartsWith("ai518") || pcode.ToLower().StartsWith("ai806"))
            {
                deviceCapacity.UserCapacity = 5000;
            }

            deviceCapacity.FaceCapacity = deviceCapacity.UserCapacity;
            deviceCapacity.FingerPrintCapacity = deviceCapacity.FaceCapacity * 2;

            if (!pcode.ToLower().Contains("fp"))
                deviceCapacity.FingerPrintCapacity = 0;
            
            var facePattern= @"f\d+";
            var hasFaceAbility=Regex.IsMatch(pcode, facePattern);
            if (!hasFaceAbility)
                deviceCapacity.FaceCapacity = 0;
            
        }

        public DeviceCapacity GetCapacity()
        {
            var mc = 0;
            var uc = 0;
            var fp = 0;
            var pc = 0;
            var slog = 0;
            var glog = 0;
            var w = 0;

            client.GetDeviceStatus(_machineNo, 1, ref mc);
            client.GetDeviceStatus(_machineNo, 2, ref uc);
            client.GetDeviceStatus(_machineNo, 3, ref fp);
            client.GetDeviceStatus(_machineNo, 4, ref pc);
            client.GetDeviceStatus(_machineNo, 5, ref slog);
            client.GetDeviceStatus(_machineNo, 8, ref glog);
            client.GetDeviceStatus(_machineNo, 7, ref w);
            
            
            
            var capacity= new DeviceCapacity()
            {
                ManagerCount = mc,
                UserCount = uc,
                FingerPrintCount = fp,
                GLogCount = glog,
                PasswordCount = pc,
                SLogCount = slog,
                WhatCount = w
            };

            setCapacity(capacity);

            return capacity;
        }


        public DeviceInfo GetDeviceInfo()
        {
            var mc = 0;
            var di = 0;
            var lang = 0;
            var pwd = 0;
            var lok = 0;
            var gw = 0;
            var sw = 0;
            var rvt = 0;
            var bi = 0;
            var ds = 0;
            client.GetDeviceInfo(_machineNo, 1, ref mc);
            client.GetDeviceInfo(_machineNo, 2, ref di);
            client.GetDeviceInfo(_machineNo, 3, ref lang);
            client.GetDeviceInfo(_machineNo, 4, ref pwd);
            client.GetDeviceInfo(_machineNo, 5, ref lok);
            client.GetDeviceInfo(_machineNo, 6, ref gw);
            client.GetDeviceInfo(_machineNo, 7, ref sw);
            client.GetDeviceInfo(_machineNo, 8, ref rvt);
            client.GetDeviceInfo(_machineNo, 9, ref bi);
            client.GetDeviceInfo(_machineNo, 10, ref ds);

            return new DeviceInfo()
            {
                MessageCount = mc,
                DeviceId = di,
                Baudrate_ID = bi,
                DateSeperate = ds,
                GlogWarning = gw,
                Language = lang,
                LockOperate = lok,
                PowerOffTime = pwd,
                ReVerifyTime = rvt,
                SlogWarning = sw
            };
        }

        public string GetDeviceTime()
        {
            int year = 0; int month = 0; int day = 0; int hours = 0; int minutes = 0; int seconds = 0;

            var result = client.GetDeviceTime(_machineNo, ref year, ref month, ref day, ref hours, ref minutes, ref seconds);
            if (!result)
                return null;

            return $"{year}/{month}/{day} {hours}:{minutes}:{seconds}";
        }

        public string GetSerialNo()
        {
            string serial = "";
            client.GetSerialNumber(_machineNo, ref serial);

            return serial;
        }

        public void PowerOn()
        {
            client.PowerOnAllDevice();
        }

        public void PowerOff()
        {
            client.PowerOffDevice(_machineNo);
        }


        public string GetProductCode()
        {
            string pcode = "";
            client.GetProductCode(_machineNo, ref pcode);
            return pcode;
        }

        public void ClearAllData()
        {
            client.CleanHoliday(_machineNo);
            client.ClearKeeperData(_machineNo);
            client.ClearUserCtrl(_machineNo);
        }

        public bool SetUserUTF8(User user)
        {
            object obj = new System.Runtime.InteropServices.VariantWrapper(user.Name);
            var bret = client.SetUserNameUTF8(0, _machineNo, (int)user.Id, 1, ref obj);

            return bret;
        }

        private int GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(2000, 1, 1, 0, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }
        private DateTime GetDateTime(int timeStamp)
        {
            DateTime dtStart =new DateTime(2000, 1, 1, 0, 0, 0);
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }

        public bool IsAI()
        {
            var devInfo = GetProductCode();
            return devInfo.ToLower().StartsWith("ai");
        }
        public bool SetUser(User user)
        {
            
            if (!IsAI())
            {
                object obj = new System.Runtime.InteropServices.VariantWrapper(user.Name);
                var bret1 = client.SetUserName(0, _machineNo, (int)user.Id, 1, ref obj);
                SetCardNo((int) user.Id, user.Privilege, (int) user.Id);
                return bret1;
            }
            
            var userInfo = new UserInfo();
            userInfo.Name = user.Name;
            userInfo.Enabled = user.Enabled?1:0;
            var bret = SetUserInfo(user.Id,userInfo);
            return bret;
        }

        public bool ModifyPrivilege(long userId,int privilege)
        {

            if (!IsAI())
            {
                var bret1 = client.ModifyPrivilege(_machineNo, (int)userId,1, 12, privilege);

                return bret1;
            }
            var userInfo = GetUserInfo(userId);

            if (userInfo == null)
                return false;
            
            userInfo.Privilege = privilege;

            var bret = SetUserInfo(userId, userInfo);
            return bret;
        }

        public bool SetCardNo(long userId,int privelege,int cardNo)
        {
            var bret = false;
            DisableDevice();

            if (IsAI())
            {
                EnableDevice();

                var userInfo = GetUserInfo(userId);

                if (userInfo == null)
                {
                    EnableDevice();
                    return false;
                }

                userInfo.Card = cardNo;
                SetUserInfo(userId,userInfo);
                return bret;
            }
            int[] FacedwData = new int[1888 / 4];
            object obj2 = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
            bret = client.SetEnrollData(_machineNo, (int)userId, 1, 11, privelege, ref obj2, cardNo);
            EnableDevice();

            return bret;
        }


        public bool SetPassword(long userId, int privilege, int password)
        {
            if (!IsAI())
            {
                int[] FacedwData = new int[1888 / 4];
                object obj2 = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
                var bret1 = client.SetEnrollData(-_machineNo, (int)userId,
                    1, 10, privilege, ref obj2, password);

                return bret1;
            }
            
            var userInfo = GetUserInfo(userId);
            userInfo.Privilege = privilege;
            userInfo.Password = password;

            var bret = SetUserInfo(userId, userInfo);
            return bret;
        }


        public string GetName(long id)
        {
            string strName = "";
            object obj = new System.Runtime.InteropServices.VariantWrapper(strName);
            object enrollId = new System.Runtime.InteropServices.VariantWrapper(id);


            var userInfo = GetUserInfo(id);
            if (userInfo==null)
                return null;

            return userInfo.Name;
        }
        
        /// <summary>
        /// working with old desvices like F631
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {

            int dwEnrollNumber = 0;
            int dwEnMachineID = 0;
            int dwBackupNum = 0;
            int dwPrivilegeNum = 0;
            int dwEnable = 0;
            int dwPassWord = 0;
            int vPhotoSize = 0;
            
            
            bool bRet;
            var users = new List<User>();

            client.ReadAllUserID(_machineNo);
            do
            {

                bRet = client.GetAllUserID(
                    _machineNo,
                    ref dwEnrollNumber,
                    ref dwEnMachineID,
                    ref dwBackupNum,
                    ref dwPrivilegeNum,
                    ref dwEnable
                );
                
                
                
                int[] FacedwData = new int[1888 / 4];
                object obj = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
                

                if (dwEnrollNumber == 0)
                {
                    continue;
                }

                if (users.Any(x => x.Id == dwEnrollNumber))
                {
                    continue;
                }

                var user = new User()
                {
                    Id = dwEnrollNumber,
                    Enabled = true,
                    Privilege = dwPrivilegeNum,
                    Name = "" //GetName(dwEnrollNumber)
                };

          
                users.Add(user);

            } while (bRet);

            return users;
        }

        public User GetUser(long id)
        {
            var userInfo = GetUserInfo(id);
            if (userInfo == null)
                return null;

            return new User()
            {
                Id = id,
                Enabled = userInfo.Enabled == 1,
                Privilege = userInfo.Privilege,
                Name = userInfo.Name
            };
        }

        /// <summary>
        /// Working with New Devices Like AI series
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            
            int dwEnrollNumber = 0;
            int dwEnMachineID = 0;
            int dwBackupNum = 0;
            int dwPrivilegeNum = 0;
            int dwEnable = 0;
            int dwPassWord = 0;
            int vPhotoSize = 0;
            
            var bret = client.ReadAllUserIDLongID(_machineNo);

            if (bret == false)
                return null;
            var users = new List<User>();

            do
            {
                
                string strEnrollID = "";
                object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(strEnrollID);
                
                bret = client.GetAllUserIDLongID(
                    _machineNo,
                    ref EnrollIDobj,
                    ref dwBackupNum,
                    ref dwPrivilegeNum,
                    ref dwEnable
                );


                var s = (string) EnrollIDobj;
                
                if(String.IsNullOrEmpty(s.Trim()))
                    continue;
                var enrollNo = long.Parse(s);

                
                
                var user = new User()
                {
                    Id = enrollNo,
                    Enabled = dwEnable == 1,
                    Privilege = dwPrivilegeNum,
                    Name = GetName(enrollNo)
                };
                
                var info = GetUserInfo(enrollNo);

                if (info != null)
                {
                    user.Style = info.UserCtrl;
                }

                users.Add(user);

            } while (bret);

            return users;
        }
        public List<GeneralLogInfo> GetAllLogs()
        {
            GeneralLogInfo gLogInfo = new GeneralLogInfo();

            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();


            var bRet = client.ReadGLogDataLongID(_machineNo,0);
            do
            {
                
                string strEnrollID = "";
                object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(strEnrollID);
                
                bRet = client.GetGLogDataLongID(_machineNo,
                ref EnrollIDobj,
                ref gLogInfo.dwVerifyMode,
                ref gLogInfo.dwInout,
                ref gLogInfo.dwEvent,
                ref gLogInfo.dwYear,
                ref gLogInfo.dwMonth,
                ref gLogInfo.dwDay,
                ref gLogInfo.dwHour,
                ref gLogInfo.dwMinute,
                ref gLogInfo.dwSecond
                );

                if (bRet)
                {
                    gLogInfo.dwEnrollNumber =long.Parse((string) EnrollIDobj);
                    myArray.Add(gLogInfo);
                }

            } while (bRet);

            return myArray;

        }


        public byte[] GetFingerPrint(long userId, int fingerIndex)
        {
            int[] dwData = new int[1420 / 4];
            int[] FacedwData = new int[1888 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(FacedwData);

            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());

            var bret = false;

            if (IsAI())
            {
                bret = client.GetFPDataLongID(_machineNo, EnrollIDobj,  fingerIndex,  ref obj);
            }
            else
            {
                int dwPassword = 0;
                int privelege = 0;
                bret = client.GetEnrollData(_machineNo, (int)userId, 1, fingerIndex, ref privelege, ref obj, ref dwPassword);
            }

            if (bret)
            {
                dwData = (int[])obj;
                byte[] _indexData = new byte[1420];
                IntPtr _ptrIndex = Marshal.AllocHGlobal(_indexData.Length);
                Marshal.Copy(dwData, 0, _ptrIndex, 1420 / 4);
                Marshal.Copy(_ptrIndex, _indexData, 0, 1420);
                Marshal.FreeHGlobal(_ptrIndex);
                return _indexData;
            }

            return null;
        }


        public bool SetFingerPrint(long userId, int fingerIndex, byte[] bytes)
        {
            int dwPassword = 0;
            object obj = new System.Runtime.InteropServices.VariantWrapper(bytes);
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());

            var result = false;

            if (IsAI())
            {
                result=client.SetFPDataLongID(_machineNo, EnrollIDobj, fingerIndex,  ref obj);
            }
            else
            {
                result = client.SetEnrollData(_machineNo, (int)userId, 1,
                    fingerIndex, 0, ref obj, dwPassword);
            }
            
            return result;
        }



        public void DeleteAllLogs()
        {
            client.EmptyGeneralLogData(_machineNo);
        }

        //Just Working on non AI Devices
        public byte[] GetFace(int userId, int index)
        {
            int dwPrivilegeNum = 0;
            int dwPassword = 0;
            int[] FacedwData = new int[1888 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(FacedwData);

            var bRet = client.GetEnrollData(
            _machineNo,
            userId,
            1,
            index,
            ref dwPrivilegeNum,
            ref obj,
            ref dwPassword
            );

            if (!bRet)
            {
                int errCode = 0;
                client.GetLastError(ref errCode);
                return null;
            }

            FacedwData = (int[])obj;
            byte[] _indexDataFace = new byte[1888];



            IntPtr _ptrIndexFace = Marshal.AllocHGlobal(_indexDataFace.Length);
            Marshal.Copy(FacedwData, 0, _ptrIndexFace, 1888 / 4);
            Marshal.Copy(_ptrIndexFace, _indexDataFace, 0, 1888);
            Marshal.FreeHGlobal(_ptrIndexFace);
            return _indexDataFace;
        }

        //just working on non Ai devices
        public bool SetFace(int userId, int faceIndex, byte[] bytes)
        {
            int dwPassword = 0;
            object obj = new System.Runtime.InteropServices.VariantWrapper(bytes);

            var result = client.SetEnrollData(_machineNo, userId, 1, faceIndex, 0, ref obj, dwPassword);
            return result;
        }

        //just working on non ai devices
        public List<byte[]> GetFaces(int userId)
        {
            var list = new List<byte[]>();
            for (int i = 20; i < 28; i++)
            {
                var bytes = GetFace(userId, i);
                list.Add(bytes);
            }

            return list;
        }


        public int GetError()
        {
            var error = 0;
            client.GetLastError(ref error);
            return error;
        }

        public List<GeneralLogInfo> GetLogs(bool marked=false)
        {
            GeneralLogInfo gLogInfo = new GeneralLogInfo();

            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();

            client.ReadMark = marked;

            var bRet = client.ReadAllGLogData(_machineNo);
            do
            {
                int id = 0;
                bRet = client.GetAllGLogDataWithSecond(_machineNo,
                ref gLogInfo.dwTMachineNumber,
                ref id,
                ref gLogInfo.dwEMachineNumber,
                ref gLogInfo.dwVerifyMode,
                ref gLogInfo.dwInout,
                ref gLogInfo.dwEvent,
                ref gLogInfo.dwYear,
                ref gLogInfo.dwMonth,
                ref gLogInfo.dwDay,
                ref gLogInfo.dwHour,
                ref gLogInfo.dwMinute,
                ref gLogInfo.dwSecond
                );

                if (bRet)
                {
                    gLogInfo.dwEnrollNumber = id;
                    myArray.Add(gLogInfo);
                }

            } while (bRet);

            return myArray;

        }

        public bool SetValidExpireDate(long userId,DateTime start, DateTime end)
        {

            if (!IsAI())
            {
                var result1=client.SetUserCtrl(_machineNo, (int)userId, 0, 0, start.Year, start.Month, start.Day,
                    end.Year, end.Month, end.Day);

                return result1;
            }

            var userInfo = GetUserInfo(userId);
            if (userInfo == null)
                return false;
            
            userInfo.StartTime=GetTimeStamp(start);
            userInfo.EndTime=GetTimeStamp(end);


            var result = SetUserInfo(userId, userInfo);
            return result;
        }


        public bool UploadPhoto(int machineNo,long userId,byte[] photo)
        {
            int vPhotoSize = photo.Length;

            int[] indexDataFacePhoto = new int[400800];
            IntPtr ptrIndexFacePhoto = Marshal.AllocHGlobal(indexDataFacePhoto.Length);
            
                
            Marshal.Copy(photo, 0, ptrIndexFacePhoto, photo.Length);        
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());

            var bRet = client.SetEnrollPhotoCSLongID(machineNo, EnrollIDobj, photo.Length, ptrIndexFacePhoto);
            return bRet;
        }
        public string DownloadPhoto(int machineNo,long userId)
        {
            int vPhotoSize = 0;
            int[] FacedwData = new int[1888 / 4];
            int[] indexDataFacePhoto = new int[400800];
            object obj = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
            IntPtr ptrIndexFacePhoto = Marshal.AllocHGlobal(indexDataFacePhoto.Length);
            
            var photoRet = GetEnrollPhotoCS(machineNo, userId, ref vPhotoSize, ptrIndexFacePhoto);
            if (photoRet)
            {
                byte[] mbytCurEnrollData = new byte[vPhotoSize];
                Marshal.Copy(ptrIndexFacePhoto, mbytCurEnrollData, 0, vPhotoSize);
                var base64 = Convert.ToBase64String(mbytCurEnrollData);
                return base64;

            }

            return null;
        }
        private bool GetEnrollPhotoCS(int machineNo,long enrollNo,ref int photoSize,IntPtr enrollPhoto)
        {
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(enrollNo.ToString());

            var photo = client.GetEnrollPhotoCSLongID(machineNo, EnrollIDobj, ref photoSize, enrollPhoto);
            return photo;
        }

        private bool RemoteAction(long userId, int backupNo)
        {

            var userInfo = GetUserInfo(userId);
            if (userInfo == null)
                return false;
            
            object EnrollIDobj = new System.Runtime.InteropServices.VariantWrapper(userId.ToString());
            object Usernameobj = new System.Runtime.InteropServices.VariantWrapper(userInfo.Name);

            bool bRet = client.AddUser(_machineNo,
                ref EnrollIDobj,
                backupNo,
                userInfo.Privilege,
                ref Usernameobj
            );

            return bRet;

        }
        public bool RemoteFingerPrint(long userId, int fingerIndex)
        {
            return RemoteAction(userId, fingerIndex);
        }
        public bool RemoteFaceScan(long userId)
        {
            return RemoteAction(userId, 50);

        }

        private UserInfo GetUserInfo(long id)
        {
            var strName = "";
            object obj = new System.Runtime.InteropServices.VariantWrapper(strName);
            object enrollId = new System.Runtime.InteropServices.VariantWrapper(id.ToString());
            
            int Password=0;
            int Card=0;
            int FaceFlag=0;
            int FPFlag=0;
            int PostID=0;
            int Privilege=0;
            int Enabled=0;
            int ShiftID=0;
            int ZoneID=0;
            int GroupID=0;
            int UserCtrl=0;	
            int StartTime=0;
            int EndTime=0;
            int BirthDay=0;
            long Card1 = 0;
            
            bool bRet = client.GetUserInfoLongID(_machineNo,
                ref enrollId,
                ref obj,
                ref Password,
                ref Card,
                ref FaceFlag,
                ref FPFlag,
                ref PostID,
                ref Privilege,
                ref Enabled,
                ref ShiftID,
                ref ZoneID,
                ref GroupID,
                ref UserCtrl,
                ref StartTime,
                ref EndTime,
                ref BirthDay
            );

            if (!bRet)
                return null;

            var userInfo = new UserInfo()
            {
                BirthDay = BirthDay,
                FaceFlag = FaceFlag,
                Password = Password,
                Privilege = Privilege,
                Card = Card,
                Card1 = Card1,
                Enabled = Enabled,
                EndTime = EndTime,
                StartTime = StartTime,
                UserCtrl = UserCtrl,
                FPFlag = FPFlag,
                GroupID = GroupID,
                PostID = PostID,
                ShiftID = ShiftID,
                ZoneID = ZoneID,
                Name = (string)obj
            };


            return userInfo;
        }

        public string GetUserProfile(long userId)
        {
            var message = "";
            object obj = new System.Runtime.InteropServices.VariantWrapper(message);


            bool bRet = client.SetUserProfile(0,
                _machineNo,
                int.Parse(userId.ToString()),
                _machineNo,
                ref obj
            );

            if (bRet)
                return (string)obj;
            return null;
            
        }
        
        public bool SetUserProfile(long userId, string message)
        {
            object obj = new System.Runtime.InteropServices.VariantWrapper(message);


            bool bRet = client.SetUserProfile(1,
                _machineNo,
                int.Parse(userId.ToString()),
                _machineNo,
                ref obj
            );

            return bRet;
        }
        
    }

    public class UserInfo
    {
        public int Password { get; set; }
    
        public int Card { get; set; }
        public int FaceFlag { get; set; }
        public int FPFlag { get; set; }
        public int PostID { get; set; }
        public int Privilege { get; set; }
        public int Enabled { get; set; }
        public int ShiftID { get; set; }
        public int ZoneID { get; set; }
        public int GroupID { get; set; }
        public int UserCtrl { get; set; }	
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int BirthDay { get; set; }
        public long Card1  { get; set; }
        public string Name { get; set; }
    }
}
