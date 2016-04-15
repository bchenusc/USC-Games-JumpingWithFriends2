using UnityEngine;
using System.Collections.Generic;

public enum JWFMenuState
{
    StartScreen = 0,
    GameLobby = 1
}

public static class JWFMenuDefines
{
    public static List<Vector3> CameraMenuPositions = new List<Vector3>()
    {
        new Vector3( 0, 0.71f,-10.36f),
        new Vector3(10,0.71f,-10.36f),
    };
}
