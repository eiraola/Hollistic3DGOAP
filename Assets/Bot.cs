using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
    {
        RaycastHit hitInfo = new RaycastHit();
        NavMeshAgent agent;

        private void Awake()
        {
            
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
             if (Input.GetMouseButtonDown(0))
             {
                 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 if (Physics.Raycast(ray.origin ,ray.direction, out hitInfo))
                 {
                    agent.destination = hitInfo.point;
                 }
             }
        }

        private void FixedUpdate()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnDestroy()
        {
            
        }
    }
