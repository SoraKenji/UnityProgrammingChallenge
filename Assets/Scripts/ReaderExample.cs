using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ReaderExample : MonoBehaviour
{
    public int countRow = 1;
    public Text uIText;
    public Text uiTitle;
    public Transform headersParents;
    public GameObject Row;
    public Transform ContentTable;
    void Start()
    {
        ReadJsonData();
    }
    public void ReadJsonData()
    {
        try{
            using(StreamReader stream = new StreamReader(Application.streamingAssetsPath + "/JsonChallenge.json")){
                string jsonStringData = stream.ReadToEnd();
                TitleHeaders _titleHeadersData = JsonConvert.DeserializeObject<TitleHeaders>(jsonStringData);
                uiTitle.text = _titleHeadersData.Title;
                countRow = 1;
                bool setHeaders = false;
                ContentTable.gameObject.SetActive(true);
                foreach(Dictionary<string, string> dataInCh in _titleHeadersData.Data){
                    GameObject uiRow = null;
                    if ((ContentTable.childCount - 1) < _titleHeadersData.Data.Count){
                        uiRow = Instantiate(Row, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        uiRow.transform.SetParent(ContentTable);
                        uiRow.name = uiRow.name+'_'+countRow;
                    }else{
                        uiRow = ContentTable.GetChild(countRow).gameObject;
                        uiRow.gameObject.SetActive(true);
                    }
                    int countHeaders = 0;
                    foreach(string headerName in _titleHeadersData.ColumnHeaders){
                        Text uiTextRow = null;
                        if (!setHeaders){
                            Text uiTextHeader = null;
                            if (headersParents.childCount < _titleHeadersData.ColumnHeaders.Length){
                                uiTextHeader = Instantiate(uIText, new Vector3(0, 0, 0), Quaternion.identity) as Text; 
                                uiTextHeader.fontStyle = FontStyle.Bold;
                                uiTextHeader.transform.SetParent(headersParents);
                            }else{
                                uiTextHeader = headersParents.GetChild(countHeaders).GetComponent<Text>();
                            }
                            uiTextHeader.text = headerName;
                            uiTextHeader.gameObject.SetActive(true);
                        }
                        if ((uiRow.transform.childCount) < _titleHeadersData.ColumnHeaders.Length){
                            uiTextRow = Instantiate(uIText, new Vector3(0, 0, 0), Quaternion.identity) as Text;
                        }else{
                            uiTextRow = uiRow.transform.GetChild(countHeaders).GetComponent<Text>();
                            Debug.Log(countHeaders);
                        }
                        if (dataInCh[headerName] == null){
                            uiTextRow.text = "";
                        }else{
                            uiTextRow.text = dataInCh[headerName];
                        }
                        uiTextRow.transform.SetParent(uiRow.transform);
                        uiTextRow.gameObject.SetActive(true); 
                        countHeaders++;
                        if(headersParents.childCount > _titleHeadersData.ColumnHeaders.Length) {
                            for(int i = _titleHeadersData.ColumnHeaders.Length; i< headersParents.childCount; i++){
                                headersParents.GetChild(i).gameObject.SetActive(false);
                            }
                        }
                        if(uiRow.transform.childCount > _titleHeadersData.ColumnHeaders.Length) {
                            for(int i = _titleHeadersData.ColumnHeaders.Length; i< headersParents.childCount; i++){
                                uiRow.transform.GetChild(i).gameObject.SetActive(false);
                            }
                        }
                    }
                    setHeaders = true;
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
            uiTitle.text = "Ocurrio un error en la lectura de datos. Este error puede deberse a que falte alguna llave en alguna de las filas.";
        }
    }
}
