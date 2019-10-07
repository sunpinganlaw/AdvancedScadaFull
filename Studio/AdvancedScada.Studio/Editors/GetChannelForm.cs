﻿using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Management;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedScada.Studio.Editors
{
    public  class GetChannelForm
    {
       

        public EventChannelChanged eventChannelChanged = null;
      

        public Management.Editors.XChannelForm XChannelFormLoad(string SelectedDrivers, ChannelService chm = null, Channel chCurrent = null)
        {
            
                return GetForm(SelectedDrivers, chCurrent, chm, "XChannelForm");
        }
       
        public Management.Editors.XChannelForm GetForm(string Path, Channel ch, ChannelService objChannelManager, string classname)
        {
            var objFunctions = GetIODriver.GetFunctions();
            var context = objFunctions.ParseNamespace($@"\AdvancedScada.{Path}.Core.dll", classname);
            var t = (Type)context;
            var ctrl = (Management.Editors.XChannelForm)objFunctions.CreateInstance(t, new object[] { Path, objChannelManager, ch });
            ctrl.eventChannelChanged += (chCurrent, isNew) =>
            {
                eventChannelChanged?.Invoke(chCurrent, isNew);
                ctrl.DialogResult = DialogResult.OK;
            };
            return ctrl;
        }

    }
    public class GetDeviceForm
    {


        public EventDeviceChanged eventDeviceChanged = null;


        public Management.Editors.XDeviceForm XDeviceFormLoad(Channel chParam, Device dvPara = null)
        {

            return GetForm(chParam.ChannelTypes, chParam, dvPara, "XDeviceForm");
        }

        public Management.Editors.XDeviceForm GetForm(string Path, Channel chParam, Device dvPara , string classname)
        {
            var objFunctions = GetIODriver.GetFunctions();
            var context = objFunctions.ParseNamespace($@"\AdvancedScada.{Path}.Core.dll", classname);
            var t = (Type)context;
           
            var newObject = (Management.Editors.XDeviceForm)objFunctions.CreateInstance(t, new object[] { chParam, dvPara });
            newObject.eventDeviceChanged += (dv, isNew) =>
            {
                eventDeviceChanged?.Invoke(dv, isNew);
                newObject.DialogResult = DialogResult.OK;
            };
            return newObject;
        }

    }
    public class GetDataBlockForm
    {


        public EventDataBlockChanged eventDataBlockChanged = null;


        public Management.Editors.XDataBlockForm XDataBlockFormLoad(Channel chParam, Device dvParam, DataBlock dbParam = null)
        {

            return GetForm(chParam.ChannelTypes, chParam, dvParam, dbParam, "XDataBlockForm");
        }

        public Management.Editors.XDataBlockForm GetForm(string Path, Channel chParam, Device dvParam, DataBlock dbParam, string classname)
        {
            var objFunctions = GetIODriver.GetFunctions();
            var context = objFunctions.ParseNamespace($@"\AdvancedScada.{Path}.Core.dll", classname);
            var t = (Type)context;

            var newObject = (Management.Editors.XDataBlockForm)objFunctions.CreateInstance(t, new object[] { chParam, dvParam, dbParam });
            newObject.eventDataBlockChanged += (db, isNew) =>
            {
                eventDataBlockChanged?.Invoke(db, isNew);
                newObject.DialogResult = DialogResult.OK;
            };
            return newObject;
        }

    }
    public class GetTagForm
    {


        public EventTagChanged eventTagChanged = null;


        public Management.Editors.XTagForm XTagFormLoad(Channel chParam, Device dvParam, DataBlock dbParam, Tag tgParam = null)
        {

            return GetForm(chParam.ChannelTypes, chParam, dvParam, dbParam, tgParam, "XTagForm");
        }

        public Management.Editors.XTagForm GetForm(string Path, Channel chParam, Device dvParam, DataBlock dbParam, Tag tgParam, string classname)
        {
            var objFunctions = GetIODriver.GetFunctions();
            var context = objFunctions.ParseNamespace($@"\AdvancedScada.{Path}.Core.dll", classname);
            var t = (Type)context;

            var newObject = (Management.Editors.XTagForm)objFunctions.CreateInstance(t, new object[] { chParam, dvParam, dbParam, tgParam });
            newObject.eventTagChanged += (tg, isNew) =>
            {
                eventTagChanged?.Invoke(tg, isNew);
                newObject.DialogResult = DialogResult.OK;
            };
            return newObject;
        }

    }
}