using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Drawing
{
    /// <summary>
    /// 封装了常用图形算法。
    /// </summary>
    public static class GraphicsArithmetic
    {
        /// <summary>
        /// 计算点到点的距离。
        /// </summary>
        /// <param name="x1">第1个点的水平坐标。</param>
        /// <param name="y1">第1个点的垂直坐标。</param>
        /// <param name="x2">第2个点的水平坐标。</param>
        /// <param name="y2">第2个点的垂直坐标。</param>
        /// <returns>两个点之间的距离。</returns>
        public static double DistancePointToPoint(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        /// <summary>
        /// 计算点到直线的最短距离(利用平行四边形的面积算法)
        /// </summary>
        /// <param name="x">目标点的水平坐标。</param>
        /// <param name="y">目标点的垂直坐标。</param>
        /// <param name="x1">直线上的第1个点的水平坐标。</param>
        /// <param name="y1">直线上的第1个点的垂直坐标。</param>
        /// <param name="x2">直线上的第2个点的水平坐标。</param>
        /// <param name="y2">直线上的第2个点的垂直坐标。</param>
        /// <returns>点到直线的距离。</returns>
        public static double DistancePointToLine(double x, double y, double x1, double y1, double x2, double y2)
        {
            //计算点到直线(a,b)的距离  
            double l = 0.0;
            double s = 0.0;
            l = DistancePointToPoint(x1, y1, x2, y2);

            s = ((y1 - y) * (x2 - x1) - (x1 - x) * (y2 - y1)) / (l * l);

            return (Math.Abs(s * l));
        }

        /// <summary>
        /// 计算点到线段的最短距离。
        /// </summary>
        /// <param name="x">目标点的水平坐标。</param>
        /// <param name="y">目标点的垂直坐标。</param>
        /// <param name="x1">线段端点1的水平坐标。</param>
        /// <param name="y1">线段端点1的垂直坐标。</param>
        /// <param name="x2">线段端点2的水平坐标。</param>
        /// <param name="y2">线段端点2的垂直坐标。</param>
        /// <returns></returns>
        public static double DistancePointToSegment(double x, double y, double x1, double y1, double x2, double y2)
        {
            double cross = (x2 - x1) * (x - x1) + (y2 - y1) * (y - y1);
            if (cross <= 0) return Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));

            double d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            if (cross >= d2) return Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));

            double r = cross / d2;
            double px = x1 + (x2 - x1) * r;
            double py = y1 + (y2 - y1) * r;
            return Math.Sqrt((x - px) * (x - px) + (py - y) * (py - y)); //return Math.Sqrt((x - px) * (x - px) + (py - y1) * (py - y1));
        }

        /// <summary>
        /// 计算两个点之间连线与0度基准线（从点（x1,y1）水平向右画线）之间的夹角角度。
        /// </summary>
        /// <param name="x1">第1个点的水平坐标。</param>
        /// <param name="y1">第1个点的垂直坐标。</param>
        /// <param name="x2">第2个点的水平坐标。</param>
        /// <param name="y2">第2个点的垂直坐标。</param>
        /// <returns>夹角角度。</returns>
        /// <remarks>返回值可能为负数。即顺时针方向的夹角角度为0到180度，逆时针方向夹角角度值在0到-180度之间。</remarks>
        public static double Angle(double x1, double y1, double x2, double y2)
        {
            return System.Math.Atan2((y2 - y1), (x2 - x1)) * 180 / Math.PI;
        }

        /// <summary>
        /// 计算两个点之间连线与0度基准线（从点（x1,y1）水平向右画线）之间的顺时针夹角角度。
        /// </summary>
        /// <param name="x1">第1个点的水平坐标。</param>
        /// <param name="y1">第1个点的垂直坐标。</param>
        /// <param name="x2">第2个点的水平坐标。</param>
        /// <param name="y2">第2个点的垂直坐标。</param>
        /// <returns>夹角角度。</returns>
        /// <remarks>返回值0到360之间的夹角角度数值。</remarks>
        public static double AbsAngle(double x1, double y1, double x2, double y2)
        {
            double b = Angle(x1, y1, x2, y2);
            double b2 = b < 1 ? 360 + b : b;
            return b2;
        }

        #region 计算线段中点。
        /// <summary>
        /// 计算线段中点。
        /// </summary>
        /// <param name="ptA">线段的端点A。</param>
        /// <param name="ptB">线段的端点B。</param>
        /// <returns>线段上的中点坐标。</returns>
        public static System.Drawing.Point SegmentMiddlePoint(System.Drawing.Point ptA, System.Drawing.Point ptB)
        {
            return new System.Drawing.Point((ptA.X + ptB.X) / 2, (ptA.Y + ptB.Y) / 2);
        }

        /// <summary>
        /// 计算线段中点。
        /// </summary>
        /// <param name="ptA">线段的端点A。</param>
        /// <param name="ptB">线段的端点B。</param>
        /// <returns>线段上的中点坐标。</returns>
        public static System.Drawing.PointF SegmentMiddlePoint(System.Drawing.PointF ptA, System.Drawing.PointF ptB)
        {
            return new System.Drawing.PointF((ptA.X + ptB.X) / 2, (ptA.Y + ptB.Y) / 2);
        }
        #endregion

    }
}
