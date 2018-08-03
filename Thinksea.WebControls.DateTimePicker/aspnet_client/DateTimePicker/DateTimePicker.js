//=============================================================
//Copyright:2007 Thinksea
//=============================================================
var DateTimePicker_style = " .ctlDatetimePicker {margin: 0px; padding: 0px; border: 0px none;}";
DateTimePicker_style += " .ctlDatetimePicker *{margin: 0px; padding: 0px; border: 0px none; font-size:12px; line-height: 20px;}";
DateTimePicker_style += " .ctlDateSelector {width: 200px; margin: 0px; padding: 5px; border: 1px solid #799AE1; background-color: #E9EFFC;}";
DateTimePicker_style += " .ctlDateSelector *{margin: 0px; padding: 0px; border: 0px none; white-space:nowrap; font-size: 14px; font-family: 宋体, Arial; background-color: #E9EFFC; color: black; line-height: 20px;}";
DateTimePicker_style += " .ctlDateSelector .selMonth option{margin: 0px; padding: 0px; border: 0px none; white-space:nowrap; font-size: 14px; font-family: 宋体, Arial; background-color: white; color: black; height:20px; line-height: 20px;}";
DateTimePicker_style += " .ctlDateSelector .Header{ color: #000000; text-align: center; vertical-align:middle; width: 26px; height: 16px; font-size:14px; line-height:16px; border-bottom: 1px solid #bbbbbb;}";
DateTimePicker_style += " .ctlDateSelector .Days{ background-color:#E9EFFC; text-align: center; vertical-align:middle; width: 26px; height: 20px; font-size:14px; line-height:20px; }";
DateTimePicker_style += " .ctlDateSelector .ContentDays{ cursor: pointer; }";
DateTimePicker_style += " .ctlDateSelector .Sunday { color:red; }";
DateTimePicker_style += " .ctlDateSelector .Sturday { }";
DateTimePicker_style += " .ctlDateSelector .SelectedDay{ background-color: #A7C9F2; }";
DateTimePicker_style += " .ctlDateSelector .Today{ border: 1px solid #0066CC; color:#0066CC; }";

//=============================================================
//插入内联样式表到页面内部最靠前位置。
//=============================================================
function insertCssBefore(css) {
    var headEls = document.getElementsByTagName("head");
    if (headEls.length > 0) {
        if (document.all) {
            var oStyle;
            if (document.styleSheets.length > 0) {
                oStyle = document.styleSheets[0];
            }
            else {
                oStyle = document.createStyleSheet();
            }
            if (oStyle) {
                var a = css.split("}");
                var insertIndex = 0;
                for (var i = 0; i < a.length; i++) {
                    if (a[i] == "") continue;
                    if (oStyle.insertRule) {
                        oStyle.insertRule(a[i] + "}", insertIndex++);
                    }
                    else {
                        var ad = a[i].split("{");
                        oStyle.addRule(ad[0], ad[1], insertIndex++);
                    }
                }
            }
        } else {
            var style = document.createElement("style");
            style.type = "text/css";
            style.innerHTML = css;
            if (headEls[0].childNodes.length == 0) {
                headEls[0].appendChild(style, headEls[0].childNodes[0]);
            }
            else {
                headEls[0].insertBefore(style, headEls[0].childNodes[0]);
            }
        }
    }
    else {
        document.write("<style type='text/css'> ");
        document.write(css);
        document.write("</style>");
    }
}

insertCssBefore(DateTimePicker_style);

//=============================================================
//解决 Firefox 浏览器中 HTML 元素没有 innerText 属性的兼容性问题。
//=============================================================
if (window.navigator.userAgent.toLowerCase().indexOf("msie") < 1) { //firefox innerText define
    HTMLElement.prototype.__defineGetter__("innerText",
    function() {
        var anyString = "";
        var childS = this.childNodes;
        for (var i = 0; i < childS.length; i++) {
            if (childS[i].nodeType == 1)
            //anyString += childS[i].tagName=="BR" ? "\n" : childS[i].innerText;
                anyString += childS[i].innerText;
            else if (childS[i].nodeType == 3)
                anyString += childS[i].nodeValue;
        }
        return anyString;
    }
    );
    HTMLElement.prototype.__defineSetter__("innerText",
    function(sText) {
        this.textContent = sText;
    }
    );
}

//=============================================================
//得到某年某月共有多少天
//=============================================================
function DateTimePicker_HowManyDays(strMonth, strYear) {
    var strDate1 = strMonth + "/" + "01" + "/" + strYear;
    strMonth = parseInt(strMonth, 10) + 1;
    var strDate2 = strMonth + "/" + "01" + "/" + strYear;
    if (strMonth == 13) {
        strDate2 = "1/" + "01" + "/" + (parseInt(strYear, 10) + 1);
    }
    var date1 = new Date(strDate1);
    var date2 = new Date(strDate2);
    var days = (date2 - date1) / 24 / 60 / 60 / 1000;
    return days;
}

//=============================================================
//初始化日历
//strWeekday----本月第一天是星期几
//strTotalDays--本月共有几天
//strToday------今天是本月的几号
//=============================================================

function DateTimePicker_setDaysToCalendar(strWeekday, strTotalDays, strToday, control_id) {
    //============
    //得到一个控件
    //============

    var obj_tblTotalCalendar = document.getElementById(control_id + ":tblTotalCalendar");
    var now = new Date();
    var strYear, strMonth;
    strYear = document.getElementById(control_id + ":txtYear").value;
    strMonth = document.getElementById(control_id + ":txtMonth").value;

    var i, j, td, count = 1;
    for (i = 2; i < 8; i++) {
        for (j = 0; j < 7; j++) {
            td = obj_tblTotalCalendar.rows[i].cells[j];
            td.Active = false;
            td.innerText = "";
            td.className = "Days";
            if ((i == 2 && j < strWeekday) == false && count <= strTotalDays) {
                td.innerText = count;
                td.onclick = function() { return DateTimePicker_tdOnclick(control_id, this); };
                td.className += " ContentDays";
                if (j == 0) {
                    td.className += " Sunday";
                }
                else if (j == 6) {
                    td.className += " Sturday";
                }
                if (now.getFullYear() == strYear && now.getMonth() == strMonth - 1 && now.getDate() == count) {
                    td.className += " Today";
                }
                if (count == strToday) {
                    td.className += " SelectedDay";
                    td.Active = true;
                }
                count = count + 1;
            }
        }
    }
}

//===============================================================
//根据日期输入框中的内容创建日历
//===============================================================
function DateTimePicker_createDateBox(control_id) {
    var strYear, strMonth, strDate, strDay;
    strYear = document.getElementById(control_id + ":txtYear").value;
    strMonth = document.getElementById(control_id + ":txtMonth").value;
    strDay = document.getElementById(control_id + ":txtDay").value;
    var dtCurrent = new Date(strYear, strMonth - 1, strDay);
    strDate = dtCurrent.getDate();
    var iWeekDate, iHowManyDays, strTemp;
    iWeekDate = (new Date(strYear, strMonth - 1, 1)).getDay();
    iHowManyDays = DateTimePicker_HowManyDays(strMonth, strYear);
    DateTimePicker_setDaysToCalendar(iWeekDate, iHowManyDays, strDate, control_id);
    var currentDate = new Date();
    var ctlCurrentDate = document.getElementById(control_id + ":txtCurrentDate");
    ctlCurrentDate.innerText = currentDate.getFullYear() + "/" + (currentDate.getMonth() + 1) + "/" + currentDate.getDate();
    var ctlsetCurrentDate = document.getElementById(control_id + ":setCurrentDate");
    ctlsetCurrentDate.onclick = function() {
        document.getElementById(control_id).setDate(currentDate.getFullYear(), currentDate.getMonth() + 1, currentDate.getDate());
        document.getElementById(control_id + ":DateBox").style.visibility = "hidden";
        document.getElementById(control_id + ":DateBox").style.display = "none";
    };
}

//==============================================================
//检测输入的是否是数字
//==============================================================
function DateTimePicker_CheckNum() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        alert("请输入数字");
        return false;
    }

    return true;
}


//********************************************************************************************************//
//********************************************事件函数****************************************************//
//********************************************************************************************************//


//===============================================================
//当按下弹出日历时
//===============================================================
function DateTimePicker_btn_onclick(control_id) {
    //============
    //得到一个控件
    //============
    var obj_DateBox = document.getElementById(control_id + ":DateBox");

    if (obj_DateBox.style.visibility == "hidden") {
        obj_DateBox.style.visibility = "visible";
        obj_DateBox.style.display = "";

        DateTimePicker_createDateBox(control_id);
        document.getElementById(control_id + ":selYear").value = document.getElementById(control_id + ":txtYear").value;
        document.getElementById(control_id + ":selMonth").selectedIndex = document.getElementById(control_id + ":txtMonth").value - 1;
    }
    else {
        obj_DateBox.style.visibility = "hidden";
        obj_DateBox.style.display = "none";
    }

}

//=============================================================
//当日期输入框失去输入焦点时
//=============================================================
function DateTimePicker_txt_OnBlue(control_id) {
    var cYear, cMonth, cDay, cHour, cMinute, cSecond;
    cYear = document.getElementById(control_id + ":txtYear");
    cMonth = document.getElementById(control_id + ":txtMonth");
    cDay = document.getElementById(control_id + ":txtDay");
    cHour = document.getElementById(control_id + ":txtHour");
    cMinute = document.getElementById(control_id + ":txtMinute");
    cSecond = document.getElementById(control_id + ":txtSecond");
    var strYear = 1, strMonth = 1, strDay = 1, strHour = 0, strMinute = 0, strSecond = 0;
    if (cYear != null) strYear = document.getElementById(control_id + ":txtYear").value;
    if (cMonth != null) strMonth = document.getElementById(control_id + ":txtMonth").value;
    if (cDay != null) strDay = document.getElementById(control_id + ":txtDay").value;
    if (cHour != null) strHour = document.getElementById(control_id + ":txtHour").value;
    if (cMinute != null) strMinute = document.getElementById(control_id + ":txtMinute").value;
    if (cSecond != null) strSecond = document.getElementById(control_id + ":txtSecond").value;

    var gIsEmpty = /^\s*$/;

    if (isNaN(strYear) || gIsEmpty.test(strYear)) {
        alert("您输入的年份不是数字！");
        document.getElementById(control_id + ":txtYear").focus();
        document.getElementById(control_id + ":txtYear").select();
        return;
    }

    if (isNaN(strMonth) || gIsEmpty.test(strMonth)) {
        alert("您输入的月份不是数字！");
        document.getElementById(control_id + ":txtMonth").focus();
        document.getElementById(control_id + ":txtMonth").select();
        return;
    }

    if (isNaN(strDay) || gIsEmpty.test(strDay)) {
        alert("您输入的天数不是数字！");
        document.getElementById(control_id + ":txtDay").focus();
        document.getElementById(control_id + ":txtDay").select();
        return;
    }

    if (isNaN(strHour) || gIsEmpty.test(strHour)) {
        alert("您输入的小时不是数字！");
        document.getElementById(control_id + ":txtHour").focus();
        document.getElementById(control_id + ":txtHour").select();
        return;
    }

    if (isNaN(strMinute) || gIsEmpty.test(strMinute)) {
        alert("您输入的分钟不是数字！");
        document.getElementById(control_id + ":txtMinute").focus();
        document.getElementById(control_id + ":txtMinute").select();
        return;
    }

    if (isNaN(strSecond) || gIsEmpty.test(strSecond)) {
        alert("您输入的秒不是数字！");
        document.getElementById(control_id + ":txtSecond").focus();
        document.getElementById(control_id + ":txtSecond").select();
        return;
    }

    var dtCurrent = new Date(parseInt(strYear, 10), parseInt(strMonth, 10) - 1, parseInt(strDay, 10), parseInt(strHour, 10), parseInt(strMinute, 10), parseInt(strSecond, 10));

    if (parseInt(strSecond, 10) < 0 || parseInt(strSecond, 10) > 59 || dtCurrent.getSeconds() != strSecond) {
        alert("您输入的秒数不正确！");
        document.getElementById(control_id + ":txtSecond").focus();
        document.getElementById(control_id + ":txtSecond").select();
        return;
    }

    if (parseInt(strMinute, 10) < 0 || parseInt(strMinute, 10) > 59 || dtCurrent.getMinutes() != strMinute) {
        alert("您输入的分钟不正确！");
        document.getElementById(control_id + ":txtMinute").focus();
        document.getElementById(control_id + ":txtMinute").select();
        return;
    }

    if (parseInt(strHour, 10) < 0 || parseInt(strHour, 10) > 23 || dtCurrent.getHours() != strHour) {
        alert("您输入的小时不正确！");
        document.getElementById(control_id + ":txtHour").focus();
        document.getElementById(control_id + ":txtHour").select();
        return;
    }

    if (parseInt(strDay, 10) < 1 || parseInt(strDay, 10) > 31 || dtCurrent.getDate() != strDay) {
        alert("您输入的天数不正确！");
        document.getElementById(control_id + ":txtDay").focus();
        document.getElementById(control_id + ":txtDay").select();
        return;
    }

    if (parseInt(strMonth, 10) < 1 || parseInt(strMonth, 10) > 12 || dtCurrent.getMonth() != (parseInt(strMonth, 10) - 1)) {
        alert("您输入的月份不正确！");
        document.getElementById(control_id + ":txtMonth").focus();
        document.getElementById(control_id + ":txtMonth").select();
        return;
    }

    if (parseInt(strYear, 10) < 1 || parseInt(strYear, 10) > 9999) {
        alert("您输入的年份不正确！");
        document.getElementById(control_id + ":txtYear").focus();
        document.getElementById(control_id + ":txtYear").select();
        return;
    }

    var obj_DateBox = document.getElementById(control_id + ":DateBox");

    if (obj_DateBox.style.visibility == "visible") {
        DateTimePicker_createDateBox(control_id);
        document.getElementById(control_id + ":selYear").value = document.getElementById(control_id + ":txtYear").value;
        document.getElementById(control_id + ":selMonth").selectedIndex = document.getElementById(control_id + ":txtMonth").value - 1;
    }

}


//=============================================================
//当选择下拉列表中的月份
//=============================================================
function DateTimePicker_selOnClick(control_id) {
    document.getElementById(control_id + ":txtMonth").value = document.getElementById(control_id + ":selMonth").value;
    DateTimePicker_createDateBox(control_id);
}

//=================================================================
//当日历中的年输入框失去焦点
//=================================================================
function DateTimePicker_selBlur(control_id) {
    document.getElementById(control_id + ":txtYear").value = document.getElementById(control_id + ":selYear").value;
    DateTimePicker_createDateBox(control_id);
}

//===============================================================
//当按下年份调节按钮（向上）
//===============================================================
function DateTimePicker_imgUpOnclick(control_id) {
    document.getElementById(control_id + ":selYear").value = ++document.getElementById(control_id + ":txtYear").value;
    DateTimePicker_createDateBox(control_id);
}


//===============================================================
//当按下年份调节按钮（向下）
//===============================================================
function DateTimePicker_imgDownOnclick(control_id) {
    document.getElementById(control_id + ":selYear").value = --document.getElementById(control_id + ":txtYear").value;
    DateTimePicker_createDateBox(control_id);
}

//===============================================================
//当点击日历中的日子
//===============================================================
function DateTimePicker_tdOnclick(control_id, src) {
    if (src.tagName != "TD") return false;
    if (src.innerText != "" && src.innerText != " ") {
        document.getElementById(control_id + ":txtDay").value = src.innerText;
        DateTimePicker_createDateBox(control_id);
        document.getElementById(control_id + ":DateBox").style.visibility = "hidden";
        document.getElementById(control_id + ":DateBox").style.display = "none";

    }

}

//===============================================================
//绘制代码
//===============================================================
function DateTimePicker_DrawCalendar(control_id, Year, Month, Day, Hour, Minute, Second, ImageUrl, ReadOnly, Enabled, ShowDate, ShowHour, ShowMinute, ShowSecond, ShowDataSelector, DateTimePickerUpImage, DateTimePickerDownImage) {
    document.write("<table cellspacing='0' cellpadding='0' border='0' width='0' class='ctlDatetimePicker'>");
    document.write("<tr>");
    document.write("<td style='margin: 0px; padding: 0px; border: 0px none; white-space:nowrap;'>");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + (ReadOnly ? " readonly='readonly'" : "") + " id='" + control_id + ":txtYear' name='" + control_id + ":txtYear' onblur=\"DateTimePicker_txt_OnBlue('" + control_id + "')\" style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 30px; height: 20px;" + (ShowDate ? "" : " display: none;") + "' maxLength='4' value='" + Year + "' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + " style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px; cursor: default;" + (ShowDate ? "" : " display: none;") + "' tabIndex='-1' readOnly value='年' name='text1' onfocus='this.blur();' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + (ReadOnly ? " readonly='readonly'" : "") + " id='" + control_id + ":txtMonth' name='" + control_id + ":txtMonth' onblur=\"DateTimePicker_txt_OnBlue('" + control_id + "')\" style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px;" + (ShowDate ? "" : " display: none;") + "' maxLength='2' value='" + Month + "' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + " style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width:16px; height: 20px; cursor: default;" + (ShowDate ? "" : " display: none;") + "' tabIndex='-1' readOnly value='月' name='text2' onfocus='this.blur();' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + (ReadOnly ? " readonly='readonly'" : "") + " id='" + control_id + ":txtDay' name='" + control_id + ":txtDay' onblur=\"DateTimePicker_txt_OnBlue('" + control_id + "')\" style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px;" + (ShowDate ? "" : " display: none;") + "' maxLength='2' value='" + Day + "' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + " style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px; cursor: default;" + (ShowDate ? "" : " display: none;") + "' tabIndex='-1' readOnly value='日' name='text3' onfocus='this.blur();' />");

    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + (ReadOnly ? " readonly='readonly'" : "") + " id='" + control_id + ":txtHour' name='" + control_id + ":txtHour' onblur=\"DateTimePicker_txt_OnBlue('" + control_id + "')\" style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px;" + (ShowHour ? "" : " display: none;") + "' maxLength='2' value='" + Hour + "' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + " name='text1' style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 5px; height: 20px; cursor: default;" + (ShowMinute ? "" : " display: none;") + "' tabIndex='-1' readOnly value=':' onfocus='this.blur();' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + (ReadOnly ? " readonly='readonly'" : "") + " id='" + control_id + ":txtMinute' name='" + control_id + ":txtMinute' onblur=\"DateTimePicker_txt_OnBlue('" + control_id + "')\" style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px;" + (ShowMinute ? "" : " display: none;") + "' maxLength='2' value='" + Minute + "' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + " name='text2' style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width:5px; height: 20px; cursor: default;" + (ShowSecond ? "" : " display: none;") + "' tabIndex='-1' readOnly value=':' onfocus='this.blur();' />");
    document.write("<input type='text'" + (Enabled ? "" : " disabled='disabled'") + (ReadOnly ? " readonly='readonly'" : "") + " id='" + control_id + ":txtSecond' name='" + control_id + ":txtSecond' onblur=\"DateTimePicker_txt_OnBlue('" + control_id + "')\" style='margin: 0px; padding: 0px; border: 0px none; text-align: center; font-size: 12px; line-height: 20px; background-color:Transparent; width: 16px; height: 20px;" + (ShowSecond ? "" : " display: none;") + "' maxLength='2' value='" + Second + "' />");

    document.write("</td>");
    if (ShowDate && ShowDataSelector) {
        document.write("<td style='margin: 0px; padding: 0px; border: 0px none;'>");
        document.write("<a id='" + control_id + ":btnChoose' style='margin: 0px; padding: 0px; border: 0px none; display: block; background-color:#ECE9D8; width: 20px; height:20px;'" + (!Enabled || ReadOnly ? " disabled='disabled'" : "") + (!Enabled || ReadOnly ? "" : " href='javascript:;' onclick=\"DateTimePicker_btn_onclick('" + control_id + "'); return false;\"") + " tabIndex='-1' type='button'" + (!Enabled || ReadOnly ? "" : " title='打开/关闭日期选择对话框'") + ">");
        document.write("<img src='" + ImageUrl + "' style='margin: 2px; padding: 0px; border: 0px none; width:16px;height:16px;' border='0' />");
        document.write("</a>");
        document.write("</td>");
    }
    document.write("</tr>");
    document.write("</table>");


    document.write("<div id='" + control_id + ":DateBox' style='z-index: 99999; visibility: hidden; display: none; position: absolute;' class='ctlDateSelector'>");
    document.write("<table id='" + control_id + ":tblTotalCalendar' style='width: 100%;' cellspacing='0' cellpadding='0' border='0'>");
    document.write("<tr>");
    document.write("<td colspan='7' style='padding: 5px;'>");
    document.write("<table cellspacing='0' cellpadding='0' border='0' width='100%'>");
    document.write("<tr>");
    document.write("<td width='50'>");
    document.write("<select id='" + control_id + ":selMonth' class='selMonth' style='margin: 0px; padding: 0px; border: 1px solid #707070; color: black; font-size:14px; height: 20px; line-height:20px; background-color: white;' accessKey='M' onchange=\"return DateTimePicker_selOnClick('" + control_id + "');\">");
    document.write("<option value='1' selected>一月</option>");
    document.write("<option value='2'>二月</option>");
    document.write("<option value='3'>三月</option>");
    document.write("<option value='4'>四月</option>");
    document.write("<option value='5'>五月</option>");
    document.write("<option value='6'>六月</option>");
    document.write("<option value='7'>七月</option>");
    document.write("<option value='8'>八月</option>");
    document.write("<option value='9'>九月</option>");
    document.write("<option value='10'>十月</option>");
    document.write("<option value='11'>十一月</option>");
    document.write("<option value='12'>十二月</option>");
    document.write("</select>");
    document.write("</td>");
    document.write("<td width='10'></td>");
    document.write("<td width='50'>");
    document.write("<input id='" + control_id + ":selYear' style='margin: 0px; padding: 0px; border: 1px solid #707070; color: black; width: 50px; height: 20px; text-align: center; font-size:14px; line-height:20px;background-color: white;' onkeypress='DateTimePicker_CheckNum();' onblur=\"DateTimePicker_selBlur('" + control_id + "')\" accessKey='Y' type='text' name='selYear' maxLength='4' />");
    document.write("</td>");
    document.write("<td width='0'>");
    document.write("<img id='" + control_id + ":imgUp' style='width:16px; height:8px; cursor: pointer; display:block;' onclick=\"return DateTimePicker_imgUpOnclick('" + control_id + "');\" src='" + DateTimePickerUpImage + "' title='增加年' /><img");
    document.write(" id='" + control_id + ":imgDown' style='width:16px; height:8px; cursor: pointer; display:block;' onclick=\"return DateTimePicker_imgDownOnclick('" + control_id + "');\" src='" + DateTimePickerDownImage + "' title='减少年' />");
    document.write("</td>");
    document.write("<td></td></tr></table>");
    document.write("</td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td class='Header' style='color:red;'>日</td>");
    document.write("<td class='Header'>一</td>");
    document.write("<td class='Header'>二</td>");
    document.write("<td class='Header'>三</td>");
    document.write("<td class='Header'>四</td>");
    document.write("<td class='Header'>五</td>");
    document.write("<td class='Header'>六</td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("<td></td>");
    document.write("</tr>");
    document.write("<tr>");
    document.write("<td colspan='7'><table style='width:100%;'><tr><td style='width:100%;'><div id='" + control_id + ":setCurrentDate' style='cursor: pointer; float: right; width: 26px; height: 20px;' class='Today'></div></td><td style='white-space:nowrap; vertical-align:middle;'>今天：<span id='" + control_id + ":txtCurrentDate'></span></td></tr></table></td>");
    document.write("</tr>");
    document.write("</table>");
    document.write("</div>");

    /* 获取当前选中的时间 */
    document.getElementById(control_id).getDateTime = function() {
        var strYear, strMonth, strDay, strHour, strMinute, strSecond;
        strYear = document.getElementById(control_id + ":txtYear").value;
        strMonth = document.getElementById(control_id + ":txtMonth").value;
        strDay = document.getElementById(control_id + ":txtDay").value;
        strHour = document.getElementById(control_id + ":txtHour").value;
        strMinute = document.getElementById(control_id + ":txtMinute").value;
        strSecond = document.getElementById(control_id + ":txtSecond").value;
        return new Date(strYear, strMonth - 1, strDay, strHour, strMinute, strSecond);
    };

    /* 设置当前选中的日期 */
    document.getElementById(control_id).setDate = function(year, month, day) {
        document.getElementById(control_id + ":txtYear").value = year;
        document.getElementById(control_id + ":txtMonth").value = month;
        document.getElementById(control_id + ":txtDay").value = day;
        DateTimePicker_createDateBox(control_id);
    };

}

//===============================================================
//获取指定 DateTimePicker 控件的值。
//===============================================================
function DateTimePicker_GetDateTime(control_id) {
    var year = document.getElementById(control_id + ":txtYear").value;
    var month = document.getElementById(control_id + ":txtMonth").value;
    var day = document.getElementById(control_id + ":txtDay").value;
    var hour = document.getElementById(control_id + ":txtHour").value;
    var minute = document.getElementById(control_id + ":txtMinute").value;
    var second = document.getElementById(control_id + ":txtSecond").value;
    return year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
}