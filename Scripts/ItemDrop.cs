using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject[] itemsToDrop;
    //Need to get the spawn rates of these items
        //Current items - soap item and baby oil

        //using cumulative frequencies here
    [SerializeField] float[] pickUpRates = {0.4f, 1.0f};
    public GameObject dropItem(){
        //Will need to generate a random number
        int randomItemIndex = Random.Range(0, itemsToDrop.Length);
        float dropRate = Random.Range(0.0f,1.0f);

        for(int i=0; i<pickUpRates.Length; i++){
            //Keep going up until we find the first probability greater than our value
            if(pickUpRates[i] <= dropRate){
                //Then this is the item which we shall return to be dropped
                return itemsToDrop[i];
            }
        }

        //Returns something if the for loop fails
        return itemsToDrop[0];
    }
    
}
