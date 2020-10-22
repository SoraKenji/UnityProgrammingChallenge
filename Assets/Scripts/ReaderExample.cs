using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ReaderExample : MonoBehaviour
{
    string actualData = "";
    //Text prefab for cells in JSON data table
    public Text uIText;
    //Title text for dynamically change the JSON data table's title
    public Text uiTitle;
    //Parent transform for headers UI components
    public Transform headersParents;
    //Parent transform prefab for rows UI components
    public GameObject Row;
    //Parent transform for content on scroll view
    public Transform ContentTable;
    void Start()
    {
        ReadJsonData();
    }
    //Code for reading data on JSON file
    public void ReadJsonData()
    {
        try{
            //read JSON file
            using(StreamReader stream = new StreamReader(Application.streamingAssetsPath + "/JsonChallenge.json")){
                string jsonStringData = stream.ReadToEnd();
                if (jsonStringData != actualData){
                    actualData = jsonStringData;
                }else{
                    return;
                }
                TitleHeaders _titleHeadersData = JsonConvert.DeserializeObject<TitleHeaders>(jsonStringData);
                uiTitle.text = _titleHeadersData.Title;
                
                //Content Table must be active
                ContentTable.gameObject.SetActive(true);
                int countHeaders = 0;
                //Read headers
                foreach(string headerName in _titleHeadersData.ColumnHeaders){
                    Text uiTextHeader = null;
                    //If there are not enough headers ui element to fill ColumnHeaders quantity, then it will create them.
                    if (countHeaders >= headersParents.childCount){
                        uiTextHeader = Instantiate(uIText, new Vector3(0, 0, 0), Quaternion.identity) as Text; 
                        uiTextHeader.fontStyle = FontStyle.Bold;
                        uiTextHeader.transform.SetParent(headersParents);
                    }else{
                        //Search in order if there are enough headers so it can be changed if JSON file changed.
                        uiTextHeader = headersParents.GetChild(countHeaders).GetComponent<Text>();
                    }
                    uiTextHeader.text = headerName;
                    uiTextHeader.gameObject.SetActive(true);
                    countHeaders++;
                }
                if(headersParents.childCount > _titleHeadersData.ColumnHeaders.Length) {
                    for(int i = _titleHeadersData.ColumnHeaders.Length; i< headersParents.childCount; i++){
                        headersParents.GetChild(i).gameObject.SetActive(false);
                    }
                }
                int countRow = 0;
                foreach(Dictionary<string, string> dataInCh in _titleHeadersData.Data){
                    GameObject uiRow = null;
                    if (countRow >= (ContentTable.childCount - 1)){
                        uiRow = Instantiate(Row, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        uiRow.transform.SetParent(ContentTable);
                        uiRow.name = uiRow.name+'_'+countRow;
                    }else{
                        uiRow = ContentTable.GetChild(countRow+1).gameObject;
                        uiRow.gameObject.SetActive(true);
                    }
                    int countElementsRow = 0;
                    foreach(string headerName in _titleHeadersData.ColumnHeaders){
                        Text uiTextRow = null;
                        if (countElementsRow >= uiRow.transform.childCount){
                            uiTextRow = Instantiate(uIText, new Vector3(0, 0, 0), Quaternion.identity) as Text;
                        }else{
                            uiTextRow = uiRow.transform.GetChild(countElementsRow).GetComponent<Text>();
                            Debug.Log(countElementsRow);
                        }
                        if (!dataInCh.ContainsKey(headerName)){
                            uiTextRow.text = "";
                        }else{
                            uiTextRow.text = dataInCh[headerName];
                        }
                        uiTextRow.transform.SetParent(uiRow.transform);
                        uiTextRow.gameObject.SetActive(true);
                        countElementsRow++;
                    }
                    if(uiRow.transform.childCount > _titleHeadersData.ColumnHeaders.Length) {
                        for(int i = _titleHeadersData.ColumnHeaders.Length; i < uiRow.transform.childCount; i++){
                            uiRow.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    countRow++;
                }
                if((ContentTable.childCount - 1) > _titleHeadersData.Data.Count){
                    for(int i = _titleHeadersData.Data.Count+1; i< ContentTable.childCount; i++){
                        ContentTable.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }catch(Exception ex){
            Debug.Log(ex);
            ContentTable.gameObject.SetActive(false);
            uiTitle.text = "An error ocurred. It may be due to some discrepancies between ColumnHeaders' quantity/names and Data array elements' parameters quantity/names.";
        }
    }
}
