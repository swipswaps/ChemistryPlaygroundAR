using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;

public class AtomView : MonoBehaviour, Node
{

    private List<Binding> bindings = new List<Binding>();

    [SerializeField]
    private AtomScriptableObject atomData;

    private List<Electron> electrons = new List<Electron>();

    private static int _nodeNumber;

    public string NodeCode
    {
        get { return atomData.code; }
        private set { }
    }

    public int NodeNumber
    {
        get;
        private set;
    }

    void Start()
    {
        NodeNumber = _nodeNumber++;
        initCore();
        initElectrons();
    }

    void OnEnable()
    {
        var moleculesController = FindObjectOfType<MoleculesController>();
        moleculesController.RegisterAtom(this);
    }

    void OnDisable()
    {
        var moleculesController = FindObjectOfType<MoleculesController>();
        moleculesController.UnregisterAtom(this);
    }

    public void ResetBindings()
    {
        foreach (Binding binding in bindings)
        {
            Destroy(binding.gameObject);
        }
        bindings.Clear();
        electrons.ForEach((electrone) => electrone.gameObject.SetActive(true));
    }

    public void Bind(AtomView other, int electronesCount)
    {
        var binding = Instantiate(Resources.Load<GameObject>("Prefabs/Binding")).GetComponent<Binding>();
        binding.first = this;
        binding.electronesCount = electronesCount;
        binding.second = other;
        bindings.Add(binding);
        electrons.Take(electronesCount).ForEach((electrone) => electrone.gameObject.SetActive(false));
    }

    void Update()
    {

    }

    private void initCore()
    {
        var upper = atomData.neutrons > atomData.protons ? atomData.neutrons : atomData.protons;
        var newRoot = Instantiate(Resources.Load<GameObject>("Prefabs/RotatingRoot"), transform);
        for (int i = 0; i < upper; i++)
        {
            if (i < atomData.neutrons)
            {
                var neutron = Instantiate(Resources.Load<GameObject>("Prefabs/Neutron"), newRoot.transform);
                var ton = neutron.GetComponent<Coreton>();
                ton.target = newRoot.transform;
                ton.radius = Mathf.Lerp(0.15f, 0.45f, atomData.neutrons / 16);
            }
            if (i < atomData.protons)
            {
                var proton = Instantiate(Resources.Load<GameObject>("Prefabs/Proton"), newRoot.transform);
                var ton = proton.GetComponent<Coreton>();
                ton.target = newRoot.transform;
                ton.radius = Mathf.Lerp(0.15f, 0.45f, atomData.protons / 16);
            }
        }
    }

    private void initElectrons()
    {
        for (int i = 0; i < atomData.upperLevelCount; i++)
        {
            var electronGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Electron"), transform);
            var electron = electronGameObject.AddComponent<Electron>();
            electron.target = transform;
            electrons.Add(electron);
        }
    }
}