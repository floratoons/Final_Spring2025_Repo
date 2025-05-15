using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public void MusicWater()
    {
        print("Now Playing water fairy moon- galen tipton");
        gameObject.GetComponents<AudioSource>()[0].enabled = true;
        gameObject.GetComponents<AudioSource>()[1].enabled = false;
        gameObject.GetComponents<AudioSource>()[2].enabled = false;
    }

    public void MusicTadpole()
    {
        print("Now Playing tadpoles lullaby- galen tipton");
        gameObject.GetComponents<AudioSource>()[0].enabled = false;
        gameObject.GetComponents<AudioSource>()[1].enabled = true;
        gameObject.GetComponents<AudioSource>()[2].enabled = false;
    }

    public void MusicAbstract()
    {
        print("Now Playing Abstract Pets- INOYAMA-LAND, Passepartout Duo");
        gameObject.GetComponents<AudioSource>()[0].enabled = false;
        gameObject.GetComponents<AudioSource>()[1].enabled = false;
        gameObject.GetComponents<AudioSource>()[2].enabled = true;
    }


}