using System.Collections.Generic;
using UnityEngine;

public static class DadosDaBatalha
{
    public static int teto;
    public static int porta;
    public static int frente;

    public static List<GameObject> tropasTeto = new();
    public static List<GameObject> tropasFrente = new();
    public static List<GameObject> tropasPorta = new();

    public static bool venceu;
}