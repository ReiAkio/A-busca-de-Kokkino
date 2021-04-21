using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class HookManager : MonoBehaviour
{
    private GameObject[] anchorPoints;
    public Transform playerPosition_hm;
    public DistanceJoint2D distanceJoint_hm;
    public string anchorpointTag;
    // Start is called before the first frame update
    void Start()
    {
        anchorPoints = GameObject.FindGameObjectsWithTag(anchorpointTag);
    }


    void Update()
    {

    }

    //
    // Resumo:
    //    Retorna o Anchorpoint mais próximo do Player
    GameObject findAnchorInLowerRange()
    {
        int lowerIndex = 0;
        double lowerDistance = distancePlayerAnchor(anchorPoints[lowerIndex]);

        for (int i = 1; i < anchorPoints.Length; i++) {

            if (lowerDistance > distancePlayerAnchor(anchorPoints[i])) {

                lowerIndex = i;
                lowerDistance = distancePlayerAnchor(anchorPoints[i]);
            }
        }

        return anchorPoints[lowerIndex];
    }

    //
    // Resumo:
    //     Retorna a distância entre o Player e um Anchorpoint
    //
    // Parâmetros:
    //   anchor:
    //     O objeto Anchorpoint cuja distancia será calculada até o Player
    double distancePlayerAnchor(GameObject anchor)
    {
        double xPlayer = playerPosition_hm.transform.position.x;
        double yPlayer = playerPosition_hm.transform.position.y;

        double xAnchor = anchor.transform.position.x;
        double yAnchor = anchor.transform.position.y;

        return Sqrt( (Pow( (xPlayer - xAnchor), 2 ) + Pow( (yPlayer - yAnchor), 2) ) );
    }

}
