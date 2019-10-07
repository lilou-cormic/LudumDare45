using UnityEngine;

public class BuildSpace : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] SpriteRenderers = null;

    [SerializeField] LineRenderer LineOfSightRenderer = null;

    private TowerDef Tower { get; set; }

    bool _isPreviewShowing = false;

    private void OnMouseDown()
    {
        if (Tower != null || Player.Cash < GameManager.Instance.CurrentTowerDef.BuildCost)
            return;

        Tower = GameManager.Instance.CurrentTowerDef;

        GameManager.BuildTower(this);

        ClearPreview();
    }

    private void OnMouseOver()
    {
        SetLineOfSight();

        if (Tower != null)
            return;

        _isPreviewShowing = true;

        SpriteRenderers[1].sprite = GameManager.Instance.CurrentTowerDef.DisplayImages[0]; // base
        SpriteRenderers[2].sprite = GameManager.Instance.CurrentTowerDef.DisplayImages[2]; // armed

        SetPreviewColor();
    }

    private void OnMouseExit()
    {
        ClearPreview();
    }

    private void Update()
    {
        if (_isPreviewShowing)
            SetPreviewColor();
    }

    private void SetPreviewColor()
    {
        if (Player.Cash < GameManager.Instance.CurrentTowerDef.BuildCost)
        {
            SpriteRenderers[1].color = new Color(0.5f, 0f, 0f, 0.5f);
            SpriteRenderers[2].color = new Color(0.5f, 0f, 0f, 0.5f);
        }
        else
        {
            SpriteRenderers[1].color = new Color(1f, 1f, 1f, 0.5f);
            SpriteRenderers[2].color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    private void ClearPreview()
    {
        _isPreviewShowing = false;

        SpriteRenderers[1].sprite = null;
        SpriteRenderers[2].sprite = null;

        LineOfSightRenderer.enabled = false;
    }

    public void SetLineOfSight()
    {
        float radius = Tower?.Range ?? GameManager.Instance.CurrentTowerDef.Range;
        var segments = 360;
        var line = LineOfSightRenderer;
        line.useWorldSpace = false;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);

        LineOfSightRenderer.enabled = true;
    }
}
