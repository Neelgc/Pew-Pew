using UnityEngine;

public class RecoilDebugAdvanced : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("========== TEST RECUL ==========");

            var recoilScript = GetComponent<GunRecoil>();

            if (recoilScript == null)
            {
                Debug.LogError(" Aucun script GunRecoil trouvé sur ce GameObject !");
                return;
            }

            Debug.Log(" Script GunRecoil trouvé");

            var type = recoilScript.GetType();
            var pivotField = type.GetField("recoilPivot",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            if (pivotField == null)
            {
                Debug.LogError(" Le champ 'recoilPivot' n'existe pas ! Tu utilises peut-être l'ancien script.");
                Debug.LogError(" Assure-toi d'utiliser GunRecoil_Pivot.cs");
                return;
            }

            Transform pivot = pivotField.GetValue(recoilScript) as Transform;

            if (pivot == null)
            {
                Debug.LogError(" LE PIVOT EST NULL ! Il n'est pas assigné dans l'Inspector !");
                Debug.LogError(" Dans GunRecoil, assigne ton pivot dans le champ 'Recoil Pivot'");
                return;
            }

            Debug.Log($" Pivot trouvé : {pivot.name}");
            Debug.Log($" Chemin du pivot : {GetFullPath(pivot)}");

            Vector3 rotationAvant = pivot.localEulerAngles;
            Debug.Log($" Rotation AVANT : {rotationAvant}");

            recoilScript.ApplyRecoil();
            Debug.Log("ApplyRecoil() appelé !");

            Vector3 rotationApres = pivot.localEulerAngles;
            Debug.Log($" Rotation APRÈS (immédiat) : {rotationApres}");

            StartCoroutine(CheckRotationDelayed(pivot));

            CheckOtherScripts(pivot);
        }
    }

    System.Collections.IEnumerator CheckRotationDelayed(Transform pivot)
    {
        yield return new WaitForSeconds(0.1f);

        Vector3 rotation = pivot.localEulerAngles;
        Debug.Log($" Rotation après 0.1s : {rotation}");

        if (rotation.x > 0.1f || rotation.y > 0.1f || rotation.z > 0.1f)
        {
            Debug.Log(" LE PIVOT A BOUGÉ !");
            Debug.LogWarning(" Si tu ne vois PAS le mouvement, un autre script écrase probablement la rotation !");
        }
        else
        {
            Debug.LogError(" LE PIVOT N'A PAS BOUGÉ DU TOUT !");
            Debug.LogError("Causes possibles :");
            Debug.LogError("1. Recoil X/Y/Z sont tous à 0");
            Debug.LogError("2. Un autre script réinitialise la rotation immédiatement");
            Debug.LogError("3. Le pivot a des contraintes (Rigidbody ?)");
        }
    }

    void CheckOtherScripts(Transform pivot)
    {
        Debug.Log("\n========== AUTRES SCRIPTS SUR LE PIVOT ==========");

        var components = pivot.GetComponents<MonoBehaviour>();

        if (components.Length == 0)
        {
            Debug.Log(" Aucun autre script sur le pivot");
            return;
        }

        foreach (var comp in components)
        {
            if (comp == null) continue;

            var scriptName = comp.GetType().Name;
            Debug.Log($" Script trouvé : {scriptName}");

            if (scriptName.Contains("Camera") ||
                scriptName.Contains("Look") ||
                scriptName.Contains("Mouse") ||
                scriptName.Contains("Controller") ||
                scriptName.Contains("Player"))
            {
                Debug.LogWarning($" '{scriptName}' pourrait ÉCRASER la rotation du recul !");
            }
        }

        var rb = pivot.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.LogWarning(" Un Rigidbody est présent sur le pivot !");
            if (rb.freezeRotation)
            {
                Debug.LogError(" Le Rigidbody a 'Freeze Rotation' activé ! Le pivot ne peut PAS tourner !");
            }
        }

        Debug.Log("================================\n");
    }

    string GetFullPath(Transform t)
    {
        string path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}