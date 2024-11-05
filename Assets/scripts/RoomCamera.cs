using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomCamera : MonoBehaviour
{
	[Header("Camera Alignment Components")]
	[SerializeField] private Grid grid;
	[SerializeField] private GameObject mainCamera;
	[SerializeField] private GameObject player;
	[SerializeField] private Tile[] tiles;
	
	[Header("Room Indication Components")]
	[SerializeField] private Tilemap mapTilemap;
	[SerializeField] private Tilemap foregroundMapTilemap;
	[SerializeField] private GameObject playerDot;

	private void Update()
    {
    	//translate the position of the player to the cell position within the large grid, which is indicating the rooms
    	Vector3Int cellPosition = grid.WorldToCell(player.transform.position);
    	foregroundMapTilemap.SetTile(cellPosition, null);
    	
    	//Ask if the map is inactive, since we only want to change the camera position, while we are playing
    	if(PlayerPrefs.GetInt("map_active")==0){
    		
    		if(mapTilemap.GetTile(cellPosition)==tiles[0]){
	//        	print("Single Size Room");
	        	mainCamera.transform.position = grid.GetCellCenterWorld(cellPosition);
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[1]){
	//        	print("Top left Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[2]){
	//        	print("Large Room top center");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[3]){
	//        	print("Top right Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[4]){
	//        	print("Large Room left");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[5]){
	//        	print("Large Room Center");
	        	Vector3 Pos = player.transform.position;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[6]){
	//        	print("Large Room Right");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[7]){
	//        	print("Bottom left Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[8]){
	//        	print("Large Room Bottom Center");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[9]){
	//        	print("Bottom right Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[10]){
	//        	print("vertival corridor top");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.x=grid.GetCellCenterWorld(cellPosition).x;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[11]){
	//        	print("vertival corridor center");
	        	Vector3 Pos = player.transform.position;
	        	Pos.x=grid.GetCellCenterWorld(cellPosition).x;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[12]){
	//        	print("vertival corridor bottom");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.x=grid.GetCellCenterWorld(cellPosition).x;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[13]){
	//        	print("horizontal corridor left");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.y=grid.GetCellCenterWorld(cellPosition).y;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[14]){
	//        	print("horizontal corridor center");
	        	Vector3 Pos = player.transform.position;
	        	Pos.y=grid.GetCellCenterWorld(cellPosition).y;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
	        else if(mapTilemap.GetTile(cellPosition)==tiles[15]){
	//        	print("horizontal corridor right");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.y=grid.GetCellCenterWorld(cellPosition).y;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	mainCamera.transform.position = Pos;
	        }
    		
    		foregroundMapTilemap.gameObject.SetActive(false);
    		
    	}
    	else{
    		
    		playerDot.transform.position = mapTilemap.GetCellCenterWorld(cellPosition);
    		foregroundMapTilemap.gameObject.SetActive(true);
    	
    	}
    }
}