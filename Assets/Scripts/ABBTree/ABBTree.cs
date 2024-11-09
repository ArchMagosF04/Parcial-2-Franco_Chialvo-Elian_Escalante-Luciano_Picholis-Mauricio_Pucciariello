using System.Collections;
using System.Collections.Generic;
using System;

public class ABBTree
{
    public Node root { get; private set; }

    public string PreOrderList;
    public string InOrderList;
    public string PostOrderList;

    public void Insert(int value)
    {
        root = Insert(root, value, 0);
    }

    protected virtual Node Insert(Node node, int value, int depth)
    {
        if (node == null)
        {
            return new Node(value, depth);
        }

        if (value < node.Value)
        {
            depth++;
            node.Left = Insert(node.Left, value, depth);
        }
        else if (value > node.Value)
        {
            depth++;
            node.Right = Insert(node.Right, value, depth);
        }
        else if (value == node.Value)
        {
            return node;
        }

        return node;
    }

    public int GetTreeHeight(Node node)
    {
        if (node == null)
        {
            return -1;
        }
        else
        {
            return (1 + Math.Max(GetTreeHeight(node.Left), GetTreeHeight(node.Right)));
        }
    }

    private void PreOrder(Node node)
    {
        if (root != null)
        {
            PreOrderList += $"{node.Value}, \n";
            if (node.Left != null)
            {
                PreOrder(node.Left);
            }
            if (node.Right != null)
            {
                PreOrder(node.Right);
            }
        }
    }

    public string ShowPreOrder()
    {
        PreOrder(root);

        string output = "Pre-Order: \n" + PreOrderList;
        PreOrderList = "";

        return output;
    }

    private void InOrder(Node node)
    {
        if (root != null)
        {
            if (node.Left != null)
            {
                InOrder(node.Left);
            }
            InOrderList += $"{node.Value}, \n";
            if (node.Right != null)
            {
                InOrder(node.Right);
            }
        }
    }

    public string ShowInOrder()
    {
        InOrder(root);

        string output = "In-Order: \n" + InOrderList;
        InOrderList = "";

        return output;
    }

    private void PostOrder(Node node)
    {
        if (root != null)
        {
            if (node.Left != null)
            {
                PostOrder(node.Left);
            }
            if (node.Right != null)
            {
                PostOrder(node.Right);
            }
            PostOrderList += $"{node.Value}, \n";
        }
    }

    public string ShowPostOrder()
    {
        PostOrder(root);

        string output = "Post-Order: \n" + PostOrderList;
        PostOrderList = "";

        return output;
    }
}
