using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingPizza : MonoBehaviour
{
    public int WithchPizza;
    Collider2D col;
    public bool HoldingPizza = false;
    public List<GameObject> Picos;
    public List<GameObject> fakePizza;
    public bool fakePizzaHolding = false;
    public int MoneyEarnFromPizza;

    void Start()
    {
        
    }

    
    void Update()
    {
        col = GetComponent<Interactions>().col;
        if (HoldingPizza) { fakePizza[WithchPizza].SetActive(true); }
    }

    public void carryingPizza()
    {
        if (HoldingPizza == false)
        { 
            WithchPizza = col.gameObject.GetComponent<Pica>().WitchPica;
            HoldingPizza = true;
            Destroy(col.gameObject);
        }
        else if (HoldingPizza)
        {
            fakePizza[WithchPizza].SetActive(false);
            Instantiate(Picos[WithchPizza],gameObject.transform.position, Quaternion.identity);
            WithchPizza = col.gameObject.GetComponent<Pica>().WitchPica;
            Destroy(col.gameObject);
            print("pica paimta");
            fakePizzaHolding = true;
        }
        
    }
    public void CustomerGetingPizza()
    {   
        
        if (HoldingPizza)
        {
            if (WithchPizza == 1)
            {
                MoneyEarnFromPizza = Picos[WithchPizza].GetComponent<Pica>().WitchPica;
                HoldingPizza = false;
            }
            else
            {
                MoneyEarnFromPizza = 1;
                HoldingPizza = false;    
            }
            print("Gavau");
        }
    }

}
