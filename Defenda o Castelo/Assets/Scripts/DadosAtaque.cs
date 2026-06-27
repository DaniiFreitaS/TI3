using System.Collections.Generic;
using UnityEngine;

public static class DadosAtaque
{
    // IDs das posições
    public static int tras;
    public static int meio;
    public static int frente;

    // Tropas spawnadas
    public static List<GameObject> tropasTras = new();
    public static List<GameObject> tropasMeio = new();
    public static List<GameObject> tropasFrente = new();

    public static void Limpar()
    {
        tras = 0;
        meio = 0;
        frente = 0;

        tropasTras.Clear();
        tropasMeio.Clear();
        tropasFrente.Clear();
    }
}