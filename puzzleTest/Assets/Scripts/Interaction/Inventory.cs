using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : Singleton<Inventory>
{
    // maximum spaces avaliable
    public int capacity;

    // list of items (in order)
    public string[] order;


    // initialize object
    // for bao to understand:
    // we make an 'empty' string array (array of strings that are "")
    // with size = capacity
    void Start(){
        order = new string[capacity];
        // initialize as list of empty strings
        for (int i = 0; i < capacity; i++){
            order[i] = "";
        }
    }


    // check if the inventory is empty
    public bool isEmpty(){
        bool empty = true;
        foreach (string item in order){
            if (!string.IsNullOrEmpty(item)){
                empty = false;
                break;
            }
        }
        return empty;
    }

    // check if the inventory is full
    public bool isFull(){
        bool full = true;
        foreach (string item in order){
            if (string.IsNullOrEmpty(item)){
                full = false;
                break;
            }
        }
        return full;
    }

    // find an empty slot in the inventory
    public int findEmptySlot(){
        for (int i = 0; i < capacity; i++){
            if (string.IsNullOrEmpty(order[i])){
                return i;
            }
        }
        return -1; // when there are no empty slots
    }

    // find index of item by name
    public int findItemIndex(string itemName){
        for (int i = 0; i < capacity; i++){
            if (order[i].Equals(itemName)){
                return i;
            }
        }
        return -1;
    }

    // adds an item by name
    public bool AddItem(string something){
        // if inventory full, don't add item
        if (isFull()){
            //Debug.Log("My pockets are full..."); //FIXME: replace with UI stuff
            return false;
        }
        // find empty slot
        int ind = findEmptySlot();
        // store item in slot
        order[ind] = something;
        //Debug.Log("Jabari put " + something + " in his pocket");
        return true;
    }

    // removes an item by name
    public void RemoveItem(string something){
        // if inventory empty, do nothing
        if (isEmpty()){
            //Debug.Log("My pockets are empty...");  //FIXME: replace with UI stuff
            return;
        }
        // find index of item
        int ind = findItemIndex(something);
        // make sure item was actually found
        if (ind < 0){
            //Debug.Log("I don't have that..."); //FIXME: replace with UI stuff
            return;
        }
        // remove item
        order[ind] = "";
    }
}
