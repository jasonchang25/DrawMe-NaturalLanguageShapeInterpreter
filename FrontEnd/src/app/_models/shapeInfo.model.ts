export interface shapeInfo{
    shape: String;
    information: { [key: string] : number};
    shapeVertices: coordinate[];
}

export interface coordinate{
    x : number;
    y : number;
}