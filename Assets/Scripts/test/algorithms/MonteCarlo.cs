using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonteCarlo : MonoBehaviour
{
    public Block BlockPrefab;
    public int Width;
    public int Height;

    UnionFind uf;

    int n;
    int topRoot;
    int bottomRoot;
    Block[] blocks;
    List<Block> availableBlocks;
    Block[,] blocksGrid;

    Button resetButton;

    void Start()
    {
        resetButton = gameObject.AddComponent<Button>();
        resetButton.onClick.AddListener(()=> {
            for (int i = 0; i < blocks.Length; i++)
                Destroy(blocks[i].gameObject);
            Create();
            Run();
        });

        Create();
        Run();
    }

    void Create()
    {
        n = Width * Height;
        uf = new UnionFind(n+2);
        topRoot =n;
        bottomRoot = n+1;
        blocks = new Block[n];
        availableBlocks = new List<Block>();
        blocksGrid = new Block[Width, Height];

        RectTransform blockPrefabRT = (RectTransform)BlockPrefab.transform;
        float x = -blockPrefabRT.sizeDelta.x * (float)Width / 2f + blockPrefabRT.sizeDelta.x / 2f;
        float y = blockPrefabRT.sizeDelta.y * (float)Height / 2f - blockPrefabRT.sizeDelta.y / 2f;

        int i = 0;
        for (int c = 0; c < Width; c++)
        {
            for (int r = 0; r < Height; r++)
            {
                Block block = Instantiate<GameObject>(BlockPrefab.gameObject).GetComponent<Block>();
                block.C = c;
                block.R = r;
                block.I = i;
                block.rectTransform.SetParent(transform, false);
                block.rectTransform.localPosition = new Vector3(
                    x + (float)c * blockPrefabRT.sizeDelta.x,
                    y - (float)r * blockPrefabRT.sizeDelta.y
                    );
                blocksGrid[c, r] = block;
                blocks[i] = block;
                availableBlocks.Add(block);
                if (r == 0)
                    uf.SetRoot(i, topRoot);
                if (r == Height - 1)
                    uf.SetRoot(i, bottomRoot);
                i++;
            }
        }
    }

    async void Run()
    {
        while (availableBlocks.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, availableBlocks.Count);
            Block block = availableBlocks[i];
            block.IsOpen = true;
            availableBlocks.RemoveAt(i);
            Union(block);

            // Mark connected to the top or the bottom
            for (int p = 0; p < blocks.Length; p++)
            {
                if (uf.Root(p) == topRoot || uf.Root(p) == bottomRoot)
                {
                    if(blocks[p].IsOpen)
                        blocks[p].image.color = new Color(0, 1, 0);
                }
            }

            // Check if complete
            if (uf.IsConnected(topRoot, bottomRoot))
            {
                Debug.Log($"It is percolate. Probability: {(blocks.Length - availableBlocks.Count) / (float)blocks.Length}");
                return;
            }
            await Task.Delay(1);
        }
    }

    void Union(Block block)
    {
        for (int c = block.C - 1; c <= block.C + 1; c += 2)
        {
            if (c >= 0 && c < Width && blocksGrid[c, block.R].IsOpen)
            {
                uf.Union(blocksGrid[c, block.R].I, block.I, topRoot);
            }
        }

        for (int r = block.R - 1; r <= block.R + 1; r += 2)
        {
            if (r >= 0 && r < Height && blocksGrid[block.C, r].IsOpen)
            {
                uf.Union(blocksGrid[block.C, r].I, block.I, topRoot);
            }
        }
    }
}
