using FP_CLOCKLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
            client.GetDeviceStatus(_machineNo, 6, ref glog);
            client.GetDeviceStatus(_machineNo, 7, ref w);


            return new DeviceCapacity()
            {
                ManagerCount = mc,
                UserCount = uc,
                FingerPrintCount = fp,
                GLogCount = glog,
                PasswordCount = pc,
                SLogCount = slog,
                WhatCount = w
            };
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


        public bool SetUser(User user)
        {
            object obj = new System.Runtime.InteropServices.VariantWrapper(user.Name);
            var bret = client.SetUserNameUTF8(0, _machineNo, user.Id, 1, ref obj);

            return bret;
        }

        public bool ModifyPrivilege(int userId,int privilege)
        {
            var bret = client.ModifyPrivilege(_machineNo, userId,1, 12, privilege);

            return bret;
        }

        public bool SetCardNo(int userId,int privelege,int cardNo)
        {
            int[] FacedwData = new int[1888 / 4];
            object obj2 = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
            var bret = client.SetEnrollData(-_machineNo, userId, 1, 11, privelege, ref obj2, cardNo);

            return bret;
        }


        public bool SetPassword(int userId, int privelege, int password)
        {
            int[] FacedwData = new int[1888 / 4];
            object obj2 = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
            var bret = client.SetEnrollData(-_machineNo, userId, 1, 11, privelege, ref obj2, password);

            return bret;
        }


        public string GetName(int id)
        {
            string strName = "";
            object obj = new System.Runtime.InteropServices.VariantWrapper(strName);

            bool bRet = client.GetUserNameUTF8(0,
              _machineNo,
              id,
              1,
              ref obj
              );

            if (!bRet)
                return null;

            return (string)obj;
        }
        public List<User> GetUsers()
        {
            client.ReadAllUserID(_machineNo);

            int dwEnrollNumber = 0;
            int dwMachineNumber = 0;
            int dwBackupNumber = 0;
            int dwUserPrivilege = 0;
            int dwAttendenceEnable = 0;
            bool bRet;
            var users = new List<User>();

            do
            {
                bRet = client.GetAllUserID(_machineNo,
               ref dwEnrollNumber,
               ref dwMachineNumber,
               ref dwBackupNumber,
               ref dwUserPrivilege,
               ref dwAttendenceEnable);

                users.Add(new User()
                {
                    Id = dwEnrollNumber,
                    Enabled = dwAttendenceEnable == 1,
                    Privilege = dwUserPrivilege,
                    Name = GetName(dwEnrollNumber)
                });

            } while (bRet);

            return users;
        }

        public List<GeneralLogInfo> GetAllLogs()
        {
            GeneralLogInfo gLogInfo = new GeneralLogInfo();

            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();


            var bRet = client.ReadAllGLogData(_machineNo);
            do
            {
                bRet = client.GetAllGLogDataWithSecond(_machineNo,
                ref gLogInfo.dwTMachineNumber,
                ref gLogInfo.dwEnrollNumber,
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
                    myArray.Add(gLogInfo);
                }

            } while (bRet);

            return myArray;

        }


        public byte[] GetFingerPrint(int userId, int fingerIndex)
        {
            int[] dwData = new int[1420 / 4];
            int[] FacedwData = new int[1888 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(FacedwData);
            int dwPassword = 0;

            int privelege = 0;

            var bret = client.GetEnrollData(_machineNo, userId, 1, fingerIndex, ref privelege, ref obj, ref dwPassword);

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


        public bool SetFingerPrint(int userId, int fingerIndex, byte[] bytes)
        {
            int dwPassword = 0;
            object obj = new System.Runtime.InteropServices.VariantWrapper(bytes);

            var result = client.SetEnrollData(_machineNo, userId, 1, fingerIndex, 0, ref obj, dwPassword);
            return result;
        }


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
                return null;

            FacedwData = (int[])obj;
            byte[] _indexDataFace = new byte[1888];



            IntPtr _ptrIndexFace = Marshal.AllocHGlobal(_indexDataFace.Length);
            Marshal.Copy(FacedwData, 0, _ptrIndexFace, 1888 / 4);
            Marshal.Copy(_ptrIndexFace, _indexDataFace, 0, 1888);
            Marshal.FreeHGlobal(_ptrIndexFace);
            return _indexDataFace;
        }

        public bool SetFace(int userId, int faceIndex, byte[] bytes)
        {
            int dwPassword = 0;
            object obj = new System.Runtime.InteropServices.VariantWrapper(bytes);

            var result = client.SetEnrollData(_machineNo, userId, 1, faceIndex, 0, ref obj, dwPassword);
            return result;
        }

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

        public List<GeneralLogInfo> GetLogs()
        {
            GeneralLogInfo gLogInfo = new GeneralLogInfo();

            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();


            var bRet = client.ReadGeneralLogData(_machineNo);
            do
            {
                bRet = client.GetGeneralLogDataWithSecond(_machineNo,
                ref gLogInfo.dwTMachineNumber,
                ref gLogInfo.dwEnrollNumber,
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
                    myArray.Add(gLogInfo);
                }

            } while (bRet);

            return myArray;

        }

    }
}
