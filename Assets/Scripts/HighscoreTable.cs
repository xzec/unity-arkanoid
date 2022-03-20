using System.Collections.Generic;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    private List<HighscoreEntry> _highscoreTable;
    private GUIStyle _cellStyle;
    private GUIStyle _toRightStyle;
    private Vector2 _panelPos;
    private const int Padding = 30;
    private const int FontSize = 22;
    private Vector2 _panelSize;
    private Rect _tableRect;
    private float _rowHeight;
    private float _columnWidth;

    private void Start()
    {
        var highscore = new Highscore();
        _highscoreTable = highscore.table;

        _panelPos = new Vector2(20, 100);
        _panelSize =
            new Vector2(Screen.width - 2 * _panelPos.x, Screen.height - 2 * _panelPos.y);
        _tableRect =
            new Rect(_panelPos.x + Padding, _panelPos.y + Padding, _panelSize.x - 2 * Padding,
                _panelSize.y - 2 * Padding);
        _rowHeight = _tableRect.height / 10;
        _columnWidth = _tableRect.width / 2;

        _cellStyle = new GUIStyle
        {
            fontSize = FontSize,
            normal = {textColor = Color.white},
            alignment = TextAnchor.MiddleLeft
        };
        _toRightStyle = new GUIStyle
        {
            fontSize = FontSize,
            normal = {textColor = Color.white},
            alignment = TextAnchor.MiddleRight
        };
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(_tableRect);
        for (var i = 0; i < _highscoreTable.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUI.Label(new Rect(0, i * _rowHeight, _columnWidth, _rowHeight), $"{i + 1}. {_highscoreTable[i].nickName}",
                _cellStyle);
            GUI.Label(new Rect(_columnWidth, i * _rowHeight, _columnWidth, _rowHeight),
                _highscoreTable[i].score.ToString(),
                _toRightStyle);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndArea();
    }
}