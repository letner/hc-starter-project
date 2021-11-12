using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualSDK.ExtensionMethods
{
    public static class Vector2Extensions
    {
        public static bool OutsideRects(this Vector2 position, Rect[] rects)
        {
            if (rects == null) {
                Debug.LogError("Vector2.OutsideRects: rects list has to be initialized!");
                return false;
            }
            var result = true;
            foreach (var rect in rects) {
                result &= position.x < rect.xMin || position.x > rect.xMax || position.y > rect.yMax || position.y < rect.yMin;
            }
            return result;
        }

        public static bool OutsideRects(this Vector2 position, List<Rect> rects)
        {
            return OutsideRects(position, rects.ToArray());
        }
    }

    public static class Vector3Extensions
    {
        public static bool OutsideRects(this Vector3 position, Rect[] rects)
        {
            if (rects == null) {
                Debug.LogError("Vector3.OutsideRects: rects list has to be initialized!");
                return false;
            }
            var result = true;
            foreach (var rect in rects) {
                result &= position.x < rect.xMin || position.x > rect.xMax || position.y > rect.yMax || position.y < rect.yMin;
            }
            return result;

        }

        public static bool OutsideRects(this Vector3 position, List<Rect> rects)
        {
            return OutsideRects(position, rects.ToArray());
        }
    }

    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            var n = list.Count;
            while (n > 1) {
                n--;
                var k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }

    public static class StringExtensions
    {
        public static string ConvertCharArrayToString(char[] array)
        {
            var result = "";
            for (var i = 0; i < array.Length; i++)
            {
                result += array[i];
            }
            return result;
        }
    }

    public static class TransformExtensions
    {
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
