using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] public NpcController npcController_1;
    [SerializeField] public NpcController2 npcController_2;
    [SerializeField] public NpcController3 npcController_3;
    
    public void Initialize()
    {
        npcController_1.Initialize();
        npcController_2.Initialize();
        npcController_3.Initialize();
    }
    
}
 