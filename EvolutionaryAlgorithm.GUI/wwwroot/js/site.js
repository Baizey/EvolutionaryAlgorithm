const primaryGraph = new Graph('primaryGraph');
const secondaryGraph = new Graph('secondaryGraph');
const ternaryGraph = new Graph('ternaryGraph');
const api = new Api();
const optionsController = new OptionsController();

async function updateView() {
    const running = await api.isRunning();
    const statistics = await api.getStatistics();
    primaryGraph.add(statistics);
    secondaryGraph.add(statistics);
    ternaryGraph.add(statistics);
    if (running) await updateView();
}

document.getElementById('button_initialize').onclick = async () => {
    await optionsController.initialize();
    const heuristic = optionsController.heuristicInput.value;
    const fitness = optionsController.fitnessInput.value;
    const geneCount = optionsController.geneCount.value - 0;
    switch (fitness) {
        case 'MinimumSpanningTree':
            primaryGraph.graph2D(await api.getNodes(), await api.getEdges());
            ternaryGraph.clear();
            break;
        case 'OneMax':
        case 'LeadingOnes':
        case 'Jump':
            primaryGraph.search(geneCount);
            ternaryGraph.clear();
            break;
    }
    switch (heuristic) {
        case 'Asymmetric':
            secondaryGraph.asymmetric();
            break;
        case 'StagnationDetection':
            secondaryGraph.mutation();
            ternaryGraph.stagnation();
            break;
        case 'HeavyTail':
        case 'MultiEndogenous':
        case 'SingleEndogenous':
            secondaryGraph.mutation();
            break;
        case 'Repair':
            secondaryGraph.lambda();
            break;
    }
}
document.getElementById('button_run').onclick = () => {
    optionsController.run().then(() => updateView());
}
document.getElementById('button_pause').onclick = () => {
    api.pause().finally();
}