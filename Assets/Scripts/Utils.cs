using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public static string GenerateRoomCode() {

        string code = "";
        for (var i = 0; i < 5; i++) {
            code += (char)('a' + Random.Range(0, 26));
        }

        return code;
    }

}
