using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ABBManager : MonoBehaviour
{
    private ABBTree tree;
    private TMP_InputField inputField;
    private TextMeshProUGUI textBox;

    [SerializeField] private GameObject nodePrefab;
    private List<GameObject> nodes = new List<GameObject>();
    [SerializeField] private int[] startingValues;
    [SerializeField] private bool useStartingValues;
    [SerializeField] private bool shouldDrawLines;

    private List<Ray2D> nodeConections = new List<Ray2D>();

    private void Awake()
    {
        tree = new ABBTree();
        inputField = GameObject.FindGameObjectWithTag("Input").GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(InsertNode);

        textBox = GameObject.FindGameObjectWithTag("TextBox").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (useStartingValues) //If true, creates a tree with the values in the array in the inspector.
        {
            for (int i = 0; i < startingValues.Length; i++)
            {
                tree.Insert(startingValues[i]);
            }

            CreateTree(tree.root, transform.position);
        }
    }

    public void InsertNode(string input) //Function that gets called whenever the value in the input field is accepted.
    {
        if (inputField.text != "")
        {
            tree.Insert(int.Parse(inputField.text));
            inputField.text = "";

            DestroyTree();
            CreateTree(tree.root, transform.position);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Show the Pre-Order text.
        {
            textBox.text = tree.ShowPreOrder();
        }
        if (Input.GetKeyDown(KeyCode.W)) //Show the In-Order text.
        {
            textBox.text = tree.ShowInOrder();
        }
        if (Input.GetKeyDown(KeyCode.E)) //Show the Post-Order text.
        {
            textBox.text = tree.ShowPostOrder();
        }
        if (Input.GetKeyDown(KeyCode.R)) //Show the tree height text.
        {
            textBox.text = "Tree Height: " + tree.GetTreeHeight(tree.root);
        }

        if (shouldDrawLines)  //Shows the Raycast connections between nodes. Only if gizmos are turned on during playtime.
        {
            foreach (var ray in nodeConections)
            {
                Debug.DrawRay(ray.origin, ray.direction * 1.7f, Color.red);
            }
        }
    }

    private void CreateTree(Node node, Vector3 position) //Instantiates nodes based on how they would be positioned in the tree (aproximatly).
    {
        if (tree.root != null)
        {
            GameObject temp = Instantiate(nodePrefab);
            temp.transform.position = position;
            temp.GetComponentInChildren<TextMeshProUGUI>().text = node.Value.ToString();
            nodes.Add(temp);
            Vector3 tempPosition = position;

            if (node.Left != null)
            {
                Vector3 leftOffset = position + new Vector3(-(2.5f - 0.3f * node.Depth), -0.8f, 0);

                Vector2 direction = (new Vector3(leftOffset.x, leftOffset.y + 0.3f, leftOffset.z) - new Vector3(position.x, position.y - 0.3f, position.z)).normalized;

                Ray2D ray = new Ray2D(new Vector2(position.x, position.y - 0.3f), direction);
                nodeConections.Add(ray);

                position = leftOffset;
                CreateTree(node.Left, position);
            }
            if (node.Right != null)
            {
                position = tempPosition;
                Vector3 rightOffset = position + new Vector3((2.5f - 0.3f * node.Depth), -0.8f, 0);

                Vector3 direction = (new Vector3(rightOffset.x, rightOffset.y + 0.3f, rightOffset.z) - new Vector3(position.x, position.y - 0.3f, position.z)).normalized;

                Ray2D ray = new Ray2D(new Vector2(position.x, position.y - 0.3f), direction);
                nodeConections.Add(ray);

                position = rightOffset;
                CreateTree(node.Right, position);
            }
        }
    }

    private void DestroyTree() //Destroys all the nodes.
    {
        if (nodes.Count > 0)
        {
            foreach (GameObject node in nodes)
            {
                Destroy(node);
            }
        }
    }
}
