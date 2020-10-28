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

document.getElementById('button_initialize').onclick = () => {
    optionsController.initialize();
    const heuristic = optionsController.heuristicInput.value;
    const fitness = optionsController.fitnessInput.value;
    const geneCount = optionsController.geneCount.value - 0;
    switch (fitness) {
        case 'OneMax':
        case 'LeadingOnes':
        case 'Jump':
            primaryGraph.search(geneCount);
            ternaryGraph.clear();
            break;
    }
    switch (heuristic) {
        case 'Asymmetric':
            // TODO: this (show R0 and R1)
            break;
        case 'HeavyTail':
        case 'MultiEndogenous':
        case 'SingleEndogenous':
        case 'StagnationDetection':
            secondaryGraph.mutation();
            break;
        case 'Repair':
            secondaryGraph.lambda();
            break;
    }
}
document.getElementById('button_run').onclick = () => {
    api.run().then(() => updateView());
}
document.getElementById('button_pause').onclick = () => {
    api.pause().finally();
}