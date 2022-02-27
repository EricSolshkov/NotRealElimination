using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace MatchEngine
{
    public class LoopCounter
    {
        private int count = 20000;
        public string info;

        public LoopCounter(string _info)
        {
            info = _info;
        }
        public bool Count
        {
            get
            {
                if (count > 0)
                {
                    --count;
                    return true;
                }
                else
                {
                    Debug.LogError("DeathLoop detected with info" + info);
                    return false;
                }
            }
        }
    }
    public sealed class Board : MonoBehaviour
    {
        [SerializeField] private TileTypeAsset[] tileTypes;

        [SerializeField] private TileTypeAsset[] textTypes;

        [SerializeField] private Row[] rows;

        [SerializeField] private AudioClip matchSound;

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private float tweenDuration;

        [SerializeField] private Transform swappingOverlay;

        [SerializeField] private bool ensureNoStartingMatches;

        private readonly List<Tile> _selection = new List<Tile>();

        private bool _isSwapping;
        private bool _isMatching;
        private bool _isShuffling;

        public event Action<string, int> OnMatch;

        private TileData[,] Matrix
        {
            get
            {
                var width = rows.Max(row => row.tiles.Length);
                var height = rows.Length;

                var data = new TileData[width, height];

                for (var y = 0; y < height; y++)
                    for (var x = 0; x < width; x++)
                        data[x, y] = GetTile(x, y).Data;

                return data;
            }
        }

        private void Start()
        {
            for (var y = 0; y < rows.Length; y++)
            {
                for (var x = 0; x < rows.Max(row => row.tiles.Length); x++)
                {
                    var tile = GetTile(x, y);

                    tile.x = x;
                    tile.y = y;

                    Generate(tile);
                    //tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];

                    tile.button.onClick.AddListener(() => Select(tile));
                }
            }

            if (ensureNoStartingMatches) StartCoroutine(EnsureNoStartingMatches());

            OnMatch += (typename, count) => Debug.Log($"Matched {count}x {typename}.");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bestMove = TileDataMatrixUtility.FindBestMove(Matrix);

                if (bestMove != null)
                {
                    Select(GetTile(bestMove.X1, bestMove.Y1));
                    Select(GetTile(bestMove.X2, bestMove.Y2));
                }
            }
        }

        private IEnumerator EnsureNoStartingMatches()
        {
            var wait = new WaitForEndOfFrame();
            var matches = TileDataMatrixUtility.FindAllMatches(Matrix);

            LoopCounter counter = new LoopCounter("EnsureNoStartingMatches");
            while (TileDataMatrixUtility.FindBestMatch(Matrix) != null && counter.Count)
            {
                foreach (var match in matches)
                {
                    foreach (var tile in match.Tiles)
                    {
                        Generate(GetTile(tile));
                    }
                }
                matches.Clear();
                matches = TileDataMatrixUtility.FindAllMatches(Matrix);
                //Shuffle();
            }
            yield return wait;
        }

        private Tile GetTile(int x, int y) => rows[y].tiles[x];
        private Tile GetTile(TileData tileData) => rows[tileData.Y].tiles[tileData.X];

        private Tile[] GetTiles(IList<TileData> tileData)
        {
            var length = tileData.Count;

            var tiles = new Tile[length];

            for (var i = 0; i < length; i++) tiles[i] = GetTile(tileData[i].X, tileData[i].Y);

            return tiles;
        }

        private async void Select(Tile tile)
        {
            if (_isSwapping || _isMatching || _isShuffling) return;

            if (!_selection.Contains(tile))
            {
                if (_selection.Count > 0)
                {
                    if (Math.Abs(tile.x - _selection[0].x) == 1 && Math.Abs(tile.y - _selection[0].y) == 0
                        || Math.Abs(tile.y - _selection[0].y) == 1 && Math.Abs(tile.x - _selection[0].x) == 0)
                        _selection.Add(tile);
                }
                else
                {
                    _selection.Add(tile);
                }
            }

            if (_selection.Count < 2) return;

            await SwapAsync(_selection[0], _selection[1]);

            if (!await TryMatchAsync()) await SwapAsync(_selection[0], _selection[1]);



            var matrix = Matrix;
            var count = new LoopCounter("Selection");
            while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
            {
                Shuffle();

                matrix = Matrix;
                if (count.Count) break;
            }

            _selection.Clear();
        }

        private async Task SwapAsync(Tile tile1, Tile tile2)
        {
            _isSwapping = true;

            var icon1 = tile1.icon;
            var icon2 = tile2.icon;
            var text1 = tile1.text;
            var text2 = tile2.text;

            var icon1Transform = icon1.transform;
            var icon2Transform = icon2.transform;
            var text1Transform = text1.transform;
            var text2Transform = text2.transform;

            icon1Transform.SetParent(swappingOverlay);
            icon2Transform.SetParent(swappingOverlay);
            text1Transform.SetParent(swappingOverlay);
            text2Transform.SetParent(swappingOverlay);

            icon1Transform.SetAsLastSibling();
            icon2Transform.SetAsLastSibling();
            text1Transform.SetAsLastSibling();
            text2Transform.SetAsLastSibling();

            var sequence = DOTween.Sequence();

            sequence.Join(icon1Transform.DOMove(icon2Transform.position, tweenDuration).SetEase(Ease.OutBack))
                    .Join(icon2Transform.DOMove(icon1Transform.position, tweenDuration).SetEase(Ease.OutBack));
            sequence.Join(text1Transform.DOMove(text2Transform.position, tweenDuration).SetEase(Ease.OutBack))
                    .Join(text2Transform.DOMove(text1Transform.position, tweenDuration).SetEase(Ease.OutBack));

            await sequence.Play()
                          .AsyncWaitForCompletion();

            icon1Transform.SetParent(tile2.transform);
            icon2Transform.SetParent(tile1.transform);
            text1Transform.SetParent(tile2.transform);
            text2Transform.SetParent(tile1.transform);

            tile1.icon = icon2;
            tile2.icon = icon1;
            tile1.text = text2;
            tile2.text = text1;

            var tile1Item = tile1.Type;
            var tile1Text = tile1.Text;

            tile1.Type = tile2.Type;
            tile1.Text = tile2.Text;

            tile2.Type = tile1Item;
            tile2.Text = tile1Text;

            _isSwapping = false;
        }

        private async Task<bool> TryMatchAsync()
        {
            var didMatch = false;

            _isMatching = true;

            var matches = TileDataMatrixUtility.FindAllMatches(Matrix);

            var c = new LoopCounter("TryMatchAsync");
            while (matches.Count != 0 && c.Count)
            {
                didMatch = true;

                var tiles = new List<Tile>();

                foreach (var match in matches)

                    foreach (var tile in match.Tiles)

                        if (!tiles.Contains(GetTile(tile)))

                            tiles.Add(GetTile(tile));

                var deflateSequence = DOTween.Sequence();

                foreach (var tile in tiles)
                {
                    deflateSequence.Join(tile.icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
                    deflateSequence.Join(tile.text.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
                }

                audioSource.PlayOneShot(matchSound);

                await deflateSequence.Play()
                                     .AsyncWaitForCompletion();

                var inflateSequence = DOTween.Sequence();

                foreach (var tile in tiles)
                {
                    Generate(tile);

                    inflateSequence.Join(tile.icon.transform.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutBack));
                    inflateSequence.Join(tile.text.transform.DOScale(0.8f * Vector3.one, tweenDuration).SetEase(Ease.OutBack));
                }

                await inflateSequence.Play()
                                     .AsyncWaitForCompletion();

                foreach (var match in matches) 
                {
                    if ( match.textMatchFlag&&match.tileMatchFlag)
                    {
                        var tileMatchType = Array.Find(tileTypes, tileType => tileType.id == match.TypeId).name;
                        var textMatchType = Array.Find(textTypes, textType => textType.id == match.TextId).name;
                        OnMatch?.Invoke(tileMatchType + " & " + textMatchType, match.Tiles.Length);
                    }
                    else if (match.tileMatchFlag) OnMatch?.Invoke(Array.Find(tileTypes, tileType => tileType.id == match.TypeId).name, match.Tiles.Length);
                    else if (match.textMatchFlag) OnMatch?.Invoke(Array.Find(textTypes, textType => textType.id == match.TextId).name, match.Tiles.Length);
                }
                    



                matches = TileDataMatrixUtility.FindAllMatches(Matrix);
            }

            _isMatching = false;

            return didMatch;
        }

        private void Generate(Tile tile)
        {
            tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];
            tile.Text = textTypes[Random.Range(0, textTypes.Length)];
        }

        private void Shuffle()
        {
            _isShuffling = true;

            foreach (var row in rows)
                foreach (var tile in row.tiles)
                    Generate(tile);

            _isShuffling = false;
        }
    }
}
