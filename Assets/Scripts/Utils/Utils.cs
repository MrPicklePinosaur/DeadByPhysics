using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Utils {

    public static string GenerateRoomCode() {

        string code = "";
        for (var i = 0; i < 5; i++) {
            code += (char)('a' + UnityEngine.Random.Range(0, 26));
        }

        return code;
    }

    //from https://coderwall.com/p/14ckza/simple-extension-for-shuffling-list
    public static void Shuffle<T>(this IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

}
