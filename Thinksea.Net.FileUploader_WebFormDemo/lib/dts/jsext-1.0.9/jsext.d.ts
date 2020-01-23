/*
对 JavaScript 原生功能进行最小扩展。
version：1.0.9
last change：2018-12-10
Author：http://www.thinksea.com/
projects url:https://github.com/thinksea/jsext
*/

interface Number {
    /**
     * 格式化数字显示方式。
     * @param pattern 格式化字符串。取值范围如下
     *     "0"零占位符。用对应的数字（如果存在）替换零；否则，将在结果字符串中显示零。
     *     "#"数字占位符。用对应的数字（如果存在）替换“#”符号；否则，不会在结果字符串中显示任何数字。
     *     "."小数点。确定小数点分隔符在结果字符串中的位置。
     *     ","组分隔符。它在各个组之间插入组分隔符字符。
     * @returns 替换后的字符串。
     * @example
     *     console.log("(123456789012.129).format()-->" + (123456789012.129).format());  //123456789012.129
     *     console.log("(123456789012.129).format('')-->" + (123456789012.129).format()); //123456789012.129
     *     console.log("(123456789012.129).format('#,##0.00')-->" + (123456789012.129).format('#,##0.00')); //123,456,789,012.13
     *     console.log("(123456789012.129).format('#,##0.##')-->" + (123456789012.129).format('#,##0.##')); //123,456,789,012.13
     *     console.log("(123456789012.129).format('#,##0.00')-->" + (123456789012.129).format('#,##0.00')); //123,456,789,012.13
     *     console.log("(123456789012.129).format('#,##0.##')-->" + (123456789012.129).format('#,##0.##')); //123,456,789,012.13
     *     console.log("(12.129).format('0.00')-->" + (12.129).format('0.00')); //12.13
     *     console.log("(12.129).format('0.##')-->" + (12.129).format('0.##')); //12.13
     *     console.log("(12).format('00000')-->" + (12).format('00000')); //00012
     *     console.log("(12).format('#.##')-->" + (12).format('#.##')); //12
     *     console.log("(12).format('#.00')-->" + (12).format('#.00')); //12.00
     *     console.log("(0).format('#.##')-->" + (0).format('#.##')); //0
     *     console.log("(123456).format('.###')-->" + (123456).format('.###')); //123456
     *     console.log("(0).format('###.#####')-->" + (0).format('###.#####')); //0
     */
    format(pattern: string): string;
}

interface Date {
    /**
     * 定义 Date.prototype.format 方法使用的本地化配置。
     */
    formatLocal: {
        "en": {
            Month: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            MonthLong: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            Week: ["Sun", "Mon", "Tue", "Web", "Thu", "Fri", "Sat"],
            WeekLong: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            AMPM: ["A", "P"],
            AMPMLong: ["AM", "PM"],
        },
        "zh_cn": {
            Month: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            MonthLong: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            Week: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
            WeekLong: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
            AMPM: ["", "午"],
            AMPMLong: ["", "午后"],

        }
    };

    /**
     * 格式化 Date 显示方式。
     * @param format 格式化字符串。取值范围参考“自定义日期和时间格式字符串”
     * @param local 语言设置。取值范围：目前只支持“en”与“zh_cn”。
     * @returns 替换后的字符串。
     *     自定义日期和时间格式字符串：
     *     “d”一个月中的某一天（1 到 31）。
     *     “dd”一个月中的某一天（01 到 31）。
     *     “ddd”一周中某天的缩写名称。
     *     “dddd”一周中某天的完整名称。
     *     “f”日期和时间值的十分之几秒。
     *     6/15/2009 13:45:30.617 -> 6
     *     6/15/2009 13:45:30.050 -> 0
     *     “ff”日期和时间值的百分之几秒。
     *     6/15/2009 13:45:30.617 -> 61
     *     6/15/2009 13:45:30.005 -> 00
     *     “fff”日期和时间值的毫秒。
     *     6/15/2009 13:45:30.617 -> 617
     *     6/15/2009 13:45:30.0005 -> 000
     *     “F”如果非零，则为日期和时间值的十分之几秒。
     *     6/15/2009 13:45:30.617 -> 6
     *     6/15/2009 13:45:30.050 ->（无输出）
     *     “FF”如果非零，则为日期和时间值的百分之几秒。
     *     6/15/2009 13:45:30.617 -> 61
     *     6/15/2009 13:45:30.005 ->（无输出）
     *     “FFF”如果非零，则为日期和时间值的毫秒。
     *     6/15/2009 13:45:30.617 -> 617
     *     6/15/2009 13:45:30.0005 ->（无输出）
     *     “h”采用 12 小时制的小时（从 1 到 12）。
     *     “hh”采用 12 小时制的小时（从 01 到 12）。
     *     “H”采用 24 小时制的小时（从 0 到 23）。
     *     “HH”采用 24 小时制的小时（从 00 到 23）。
     *     “m”分钟（0 到 59）。
     *     “mm”分钟（00 到 59）。
     *     “M”月份（1 到 12）。
     *     “MM”月份（01 到 12）。
     *     “MMM”月份的缩写名称。
     *     “MMMM”月份的完整名称。
     *     “s”秒（0 到 59）。
     *     “ss”秒（00 到 59）。
     *     “t”AM/PM 指示符的第一个字符。
     *     6/15/2009 1:45:30 PM -> P
     *     6/15/2009 1:45:30 PM -> 午
     *     “tt”AM/PM 指示符。
     *     6/15/2009 1:45:30 PM -> PM
     *     6/15/2009 1:45:30 PM -> 午後
     *     “y”年份（0 到 99）。
     *     “yy”年份（00 到 99）。
     *     “yyy”年份（最少三位数字）。
     *     “yyyy”由四位数字表示的年份。
     * @example
     *     var d = new Date("2015-01-30 13:15:38.617");
     *     console.log('d.format("yyyy年 MMM dd dddd", "zh_cn")-->' + d.format("yyyy年 MMM dd dddd", "zh_cn"));    //2015年 一月 30 星期五
     *     console.log('d.format("yyyy MMM dd dddd", "en")-->' + d.format("yyyy MMM dd dddd", "en"));    //2015 Jan 30 Friday
     */
    format(pattern: string, local: string): string;

    /**
     * 格式化 Date 显示方式。
     * @param format 格式化字符串。取值范围参考“自定义日期和时间格式字符串”
     * @returns 替换后的字符串。
     *     自定义日期和时间格式字符串：
     *     “d”一个月中的某一天（1 到 31）。
     *     “dd”一个月中的某一天（01 到 31）。
     *     “ddd”一周中某天的缩写名称。
     *     “dddd”一周中某天的完整名称。
     *     “f”日期和时间值的十分之几秒。
     *     6/15/2009 13:45:30.617 -> 6
     *     6/15/2009 13:45:30.050 -> 0
     *     “ff”日期和时间值的百分之几秒。
     *     6/15/2009 13:45:30.617 -> 61
     *     6/15/2009 13:45:30.005 -> 00
     *     “fff”日期和时间值的毫秒。
     *     6/15/2009 13:45:30.617 -> 617
     *     6/15/2009 13:45:30.0005 -> 000
     *     “F”如果非零，则为日期和时间值的十分之几秒。
     *     6/15/2009 13:45:30.617 -> 6
     *     6/15/2009 13:45:30.050 ->（无输出）
     *     “FF”如果非零，则为日期和时间值的百分之几秒。
     *     6/15/2009 13:45:30.617 -> 61
     *     6/15/2009 13:45:30.005 ->（无输出）
     *     “FFF”如果非零，则为日期和时间值的毫秒。
     *     6/15/2009 13:45:30.617 -> 617
     *     6/15/2009 13:45:30.0005 ->（无输出）
     *     “h”采用 12 小时制的小时（从 1 到 12）。
     *     “hh”采用 12 小时制的小时（从 01 到 12）。
     *     “H”采用 24 小时制的小时（从 0 到 23）。
     *     “HH”采用 24 小时制的小时（从 00 到 23）。
     *     “m”分钟（0 到 59）。
     *     “mm”分钟（00 到 59）。
     *     “M”月份（1 到 12）。
     *     “MM”月份（01 到 12）。
     *     “MMM”月份的缩写名称。
     *     “MMMM”月份的完整名称。
     *     “s”秒（0 到 59）。
     *     “ss”秒（00 到 59）。
     *     “t”AM/PM 指示符的第一个字符。
     *     6/15/2009 1:45:30 PM -> P
     *     6/15/2009 1:45:30 PM -> 午
     *     “tt”AM/PM 指示符。
     *     6/15/2009 1:45:30 PM -> PM
     *     6/15/2009 1:45:30 PM -> 午後
     *     “y”年份（0 到 99）。
     *     “yy”年份（00 到 99）。
     *     “yyy”年份（最少三位数字）。
     *     “yyyy”由四位数字表示的年份。
     * @example
     *     var d = new Date("2015-01-30 13:15:38.617");
     *     console.log('d.format()-->' + d.format());    //Fri Jan 30 2015 13:15:38 GMT+0800 (中国标准时间)
     *     console.log('d.format("")-->' + d.format(""));    //Fri Jan 30 2015 13:15:38 GMT+0800 (中国标准时间)
     *     console.log('d.format("yyyy-MM-dd HH:mm:ss")-->' + d.format("yyyy-MM-dd HH:mm:ss"));    //2015-01-30 13:15:38
     *     console.log('d.format("yyyy年MM月dd日 HH:mm:ss")-->' + d.format("yyyy年MM月dd日 HH:mm:ss"));    //2015年01月30日 13:15:38
     *     console.log('d.format("yyyy-MM-dd HH:mm:ss.fff")-->' + d.format("yyyy-MM-dd HH:mm:ss.fff"));    //2015-01-30 13:15:38.617
     */
    format(pattern: string): string;

    /**
     * 增加/减少毫秒。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addMilliseconds(value: number): Date;

    /**
     * 增加/减少秒。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addSeconds(value: number): Date;

    /**
     * 增加/减少分钟。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addMinutes(value: number): Date;

    /**
     * 增加/减少小时。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addHours(value: number): Date;

    /**
     * 增加/减少天。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addDays(value: number): Date;

    /**
     * 增加/减少月。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addMonths(value: number): Date;

    /**
     * 增加/减少年。
     * @param value 一个整数，正数表示增加，负数表示减少。
     * @returns 调整后的新 Date 实例。
     */
    addYears(value: number): Date;
}

interface Array<T> {
    /**
     * 获取一个元素在 Array 中的索引值。（为 JavaScript Array 对象添加的扩展方法。）
     * @param p_var 要检索的元素。
     * @returns 元素的索引值。找不到返回 -1。
     * @example
     *     var a = new Array();
     *     a.push("abc");
     *     a.push("def");
     *     alert(a.indexOf("abc"));
     *     alert(a.indexOf("def"));
     */
    indexOf(p_var: T): number;

    /**
     * 从 Array 中删除一个元素。（为 JavaScript Array 对象添加的扩展方法。）
     * @param o 要删除的元素。
     * @returns 找到并且成功删除返回 true。否则返回 false。
     * @example
     *     var a = new Array();
     *     a.push("abc");
     *     a.push("def");
     *     alert(a[0]);
     *     a.remove("abc");
     *     alert(a[0]);
     */
    remove(o: T): boolean;
}

/**
 * 通过替换为转义码来转义最小的元字符集（\、*、+、?、|、{、[、(、)、^、$、.、# 和空白）。（为 JavaScript RegExp 对象添加的扩展方法。）
 * @param str 一个可能包含正则表达式元字符的字符串。
 * @returns 替换后的字符串。
 * @example
 *     var s="abc$def";
 *     alert(regExpEscape(s));//输出 abc\$def。
 */
declare function regExpEscape(str: string): string;


interface String {
    /**
     * 判断字符串是否以指定的文本为前缀。（为 JavaScript String 对象添加的扩展方法。）
     * @param {string} searchString 要搜索的子字符串。
     * @param {number} position 在 str 中搜索 searchString 的开始位置，默认值为 0，也就是真正的字符串开头处。
     * @returns {boolean} 如果匹配成功返回 true；否则返回 false。
     * @example
     * var str = "To be, or not to be, that is the question.";
     * alert(str.startsWith("To be"));         // true
     * alert(str.startsWith("not to be"));     // false
     * alert(str.startsWith("not to be", 10)); // true
     */
    startsWith(searchString: string, position?: number): boolean;

    /**
     * 判断字符串是否以指定的文本为后缀。（为 JavaScript String 对象添加的扩展方法。）
     * @param {string} searchString 要搜索的子字符串。
     * @param {number} position 在 str 中搜索 searchString 的结束位置，默认值为 str.length，也就是真正的字符串结尾处。
     * @returns {boolean} 如果匹配成功返回 true；否则返回 false。
     * @example
     * var str = "To be, or not to be, that is the question.";
     * alert( str.endsWith("question.") );  // true
     * alert( str.endsWith("to be") );      // false
     * alert( str.endsWith("to be", 19) );  // true
     * alert( str.endsWith("To be", 5) );   // true
     */
    endsWith(searchString: string, position?: number): boolean;

    /**
     * 从当前 String 对象移除数组中指定的一组字符的所有前导匹配项和尾部匹配项。（为 JavaScript String 对象添加的扩展方法。）
     * @param trimChars 要删除的字符的数组，或 null。如果 trimChars 为 null 或空数组，则改为删除空白字符。
     * @returns 从当前字符串的开头和结尾删除所出现的所有 trimChars 参数中的字符后剩余的字符串。 如果 trimChars 为 null 或空数组，则改为移除空白字符。
     * @example
     *     alert("aaabccdeaabaaa".trim('a')) //输出“bccdeaab”
     *     alert("aaabccdeaabaaa".trim(['a', 'b'])) //输出“ccde”
     *     alert("aaabccdeaabaaa".trim('a', 'b')) //输出“ccde”
     */
    trim(trimChars: string[]): string;

    /**
     * 从当前 String 对象移除数组中指定的一组字符的所有前导匹配项。（为 JavaScript String 对象添加的扩展方法。）
     * @param trimChars 要删除的字符的数组，或 null。如果 trimChars 为 null 或空数组，则改为删除空白字符。
     * @returns 从当前字符串的开头移除所出现的所有 trimChars 参数中的字符后剩余的字符串。 如果 trimChars 为 null 或空数组，则改为移除空白字符。
     * @example
     *     alert("aaabccdeaabaaa".trimStart('a')) //输出“bccdeaabaaa”
     *     alert("aaabccdeaabaaa".trimStart(['a', 'b'])) //输出“ccdeaabaaa”
     *     alert("aaabccdeaabaaa".trimStart('a', 'b')) //输出“ccdeaabaaa”
     */
    trimStart(trimChars: string[]): string;

    /**
     * 从当前 String 对象移除数组中指定的一组字符的所有尾部匹配项。（为 JavaScript String 对象添加的扩展方法。）
     * @param trimChars 要删除的字符的数组，或 null。如果 trimChars 为 null 或空数组，则改为删除空白字符。
     * @returns 从当前字符串的结尾移除所出现的所有 trimChars 参数中的字符后剩余的字符串。 如果 trimChars 为 null 或空数组，则改为删除空白字符。
     * @example
     *     alert("aaabccdeaabaaa".trimEnd('a')) //输出“aaabccdeaab”
     *     alert("aaabccdeaabaaa".trimEnd(['a', 'b'])) //输出“aaabccde”
     *     alert("aaabccdeaabaaa".trimEnd('a', 'b')) //输出“aaabccde”
     */
    trimEnd(trimChars: string[]): string;

    /**
     * 获取文件全名。（为 JavaScript String 对象添加的扩展方法。）
     * @returns 文件名。
     * @example
     *     console.log("c:\\a\\b\\d.e.txt".getFileName()); //d.e.txt
     *     console.log("http://www.mysite.com/b/d.e.htm?id=j.pp/ext.jpg".getFileName()); //d.e.htm
     */
    getFileName(): string;

    /**
     * 获取文件扩展名。（为 JavaScript String 对象添加的扩展方法。）
     * @returns 获取到的文件扩展名，如果有（以.为前缀）。
     * @example
     *     console.log("c:\\a\\b\\d.e.txt".getExtensionName()); //.txt
     *     console.log("http://www.mysite.com/b/d.e.htm?id=j.pp/ext.jpg".getExtensionName()); //.htm
     */
    getExtensionName(): string;

    /**
     * 从指定的 URI 中获取指定的参数的值。
     * @param name 参数名。
     * @returns 找不到返回 null，否则返回找到的值。
     */
    getUriParameter(name: string): string;

    /**
     * 为指定的 URI 设置参数。
     * @param name 参数名。
     * @param value 新的参数值。
     * @returns 处理后的 uri。
     * @description 如果参数存在则更改它的值，否则添加这个参数。
     */
    setUriParameter(name: string, value: string): string;

    /**
     * 从指定的 URI 删除参数。
     * @param name 参数名。
     * @returns 处理后的 uri。
     */
    removeUriParameter(name: string): string;

    /**
     * 从指定的 URI 删除所有参数（从问号“?”开始到字符串结束的子串），只保留问号“?”之前的部分。
     * @returns 处理后的 uri。
     */
    clearUriParameter(): string;

    /**
     * 获取指定的 URI 的协议和域名部分。
     * @returns 找不到返回空字符串 “”，否则返回找到的值。
     * @example
     *     alert("http://www.thinksea.com/a.htm".getUriProtocolAndDomain());//返回值为 http://www.thinksea.com
     *     alert("http://www.thinksea.com:8080/a.htm".getUriProtocolAndDomain());//返回值为 http://www.thinksea.com:8080
     */
    getUriProtocolAndDomain(): string;

    /**
     * 获取指定的 URI 的路径（不包含文件名和参数部分），返回结果以左下划线“/”为后缀。
     * @returns 找不到返回 null，否则返回找到的值。
     * @description 下列情况中(*.*)视为文件名
     *     1、xxxx://domain/(*.*)
     *     2、xxxx://domain/(*.*)?parameters
     *     注意：由于 URL 存在的允许特殊使用原因，下列特殊情况不包含在内，即以路径分隔符结束的情况：
     *     1、xxxx://domain/(*.*)/
     *     2、xxxx://domain/(*.*)/?parameters
     * @example
     *     alert("http://www.thinksea.com/a.aspx?id=1&name=2".getUriPath());//输出 http://www.thinksea.com/
     *     alert("http://www.thinksea.com/?id=1&name=2".getUriPath());//输出 http://www.thinksea.com/
     *     alert("http://www.thinksea.com?id=1&name=2".getUriPath());//输出 http://www.thinksea.com/
     *     alert("http://www.thinksea.com/".getUriPath());//输出 http://www.thinksea.com/
     *     alert("http://www.thinksea.com".getUriPath());//输出 http://www.thinksea.com/
     *     alert("http://www.thinksea.com/a.aspx/?id=1&name=2".getUriPath());//输出 http://www.thinksea.com/a.aspx/
     */
    getUriPath(): string;

    /**
     * 返回当前路径与指定路径的组合。
     * @param uri2 第2个 uri 字符串。
     * @returns 如果任何一个路径为空字符串，则返回另一个路径的值；如果 uri2 包含绝对路径则返回 uri2；否则返回两个路径的组合。
     * @example
     *     alert("http://www.thinksea.com/a".combineUri("b/c.htm"));//返回值为 http://www.thinksea.com/a/b/c.htm
     *     alert("http://www.thinksea.com/a".combineUri("/b/c.htm"));//返回值为 http://www.thinksea.com/b/c.htm
     */
    combineUri(uri2: string): string;

    /**
     * 获取指定 Uri 的最短路径。通过转化其中的 ../ 等内容，使其尽可能缩短。
     * @returns 处理后的 uri。
     * @example
     *     alert("http://www.thinksea.com/../../a/b/../c.htm".getFullUri());//返回值为 http://www.thinksea.com/a/c.htm
     */
    getFullUri(): string;

    /**
     * RGB格式颜色转换为16进制格式。
     * @returns 一个16进制格式的颜色值，如果无法转换则原样返回。
     * @example 十六进制颜色值与RGB格式颜色值之间的相互转换
     *     var sRgb = "RGB(255, 255, 255)", sHex = "#00538b";
     *     var sHexColor = sRgb.toColorHex();//转换为十六进制方法
     *     var sRgbColor = sHex.toColorRGB();//转为RGB颜色值的方法
     */
    toColorHex(): string;

    /**
     * 16进制格式颜色转为RGB格式。
     * @returns 一个RGB格式的颜色值，如果无法转换则原样返回。
     * @example 十六进制颜色值与RGB格式颜色值之间的相互转换
     *     var sRgb = "RGB(255, 255, 255)", sHex = "#00538b";
     *     var sHexColor = sRgb.toColorHex();//转换为十六进制方法
     *     var sRgbColor = sHex.toColorRGB();//转为RGB颜色值的方法
     */
    toColorRGB(): string;
}



/**
 * 转义一个字符串，使其符合 XML 实体规则。
 * @param str 一个文本片段。
 * @returns 符合 XML 实体规则的文本对象。
 */
declare function xmlEncode(str: string): string;


/**
 * 将字符串转换为 HTML 编码的字符串。
 * @param str 要编码的字符串。
 * @returns 编码后的 HTML 文本。
 */
declare function htmlEncode(str: string): string;

/**
 * 将已经进行过 HTML 编码的字符串转换为已解码的字符串。
 * @param str 要解码的字符串。
 * @returns 解码后的 HTML 文本。
 */
declare function htmlDecode(str: string): string;

/**
 * 判断用户端访问环境是否移动电话浏览器。
 * @returns 如果是移动电话则返回 true；否则返回 false。
 * @see  http://detectmobilebrowsers.com/ 以此站点提供的解决方案为基础进行了修改。
 */
declare function isMobile(): boolean;

/**
 * 判断用户端访问环境是否移动电话或平板浏览器。
 * @returns 如果是则返回 true；否则返回 false。
 * @see http://detectmobilebrowsers.com/ 以此站点提供的解决方案为基础进行了修改。
 * 注意：此方法存在一个已知的BUG，无法得知如何识别微软的 surface 平板设备。
 */
declare function isMobileOrPad(): boolean;

/**
 * 判断是否在微信浏览器内访问网页。
 * @returns 如果是则返回 true；否则返回 false。
 */
declare function isWeixinBrowser(): boolean;
