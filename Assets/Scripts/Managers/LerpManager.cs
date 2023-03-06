using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpManager : MonoBehaviour
{
    #region Singleton
    public static LerpManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public IEnumerator LerpPos(GameObject obj, Vector3 start, Vector3 end, float speedLerp)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speedLerp;

            obj.transform.position = Vector3.Lerp(start, end, time);
            yield return null;
        }
    }
    public IEnumerator LerpPosCurve(GameObject obj, Vector3 start, Vector3 end, float speedLerp)
    {
        Vector3 dir = (start + end) / 2f;
        Vector3 point = new Vector3(dir.x, dir.y + 10f, dir.z);

        //if (obj.GetComponent<Rigidbody>())
        //{
        //    obj.GetComponent<Rigidbody>().isKinematic = true;
        //}
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speedLerp;
            if (obj != null)
            {
                obj.transform.position = Vector3.Lerp(start, GetPoint(obj.transform.position, point, end, time), time);
            }
            
            yield return null;
        }
    }
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p02 = Vector3.Lerp(p1, p2, t);

        Vector3 p001 = Vector3.Lerp(p01, p02, t);

        return p001;
    }
    public IEnumerator LerpRot(GameObject obj, Vector3 start, Vector3 end, float speedLerp)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speedLerp;
            obj.transform.rotation = Quaternion.Lerp(Quaternion.Euler(start), Quaternion.Euler(end), time);
            yield return null;
        }
    }
    public IEnumerator LerpScale(GameObject obj, Vector3 start, Vector3 end, float speedLerp)
    {
        float time = 0f;
        while (time < 1f)
        {
            if (obj != null)
            {
                time += Time.deltaTime * speedLerp;
                obj.transform.localScale = Vector3.Lerp(start, end, time);
                yield return null;
            }
        }
    }
    public IEnumerator LerpPosLocal(GameObject obj, Vector3 start, Vector3 end, float speedLerp)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speedLerp;
            obj.transform.localPosition = Vector3.Lerp(start, end, time);
            yield return null;
        }
    }
    public IEnumerator LerpRotLocal(GameObject obj, Vector3 start, Vector3 end, float speedLerp)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speedLerp;
            obj.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(start), Quaternion.Euler(end), time);
            yield return null;
        }
    }
}
